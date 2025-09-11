
using MongoDB.Driver;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<Results<Created, ProblemHttpResult>> CreateContactMongoAsync(
    CreateContactRequest request,
    IMongoDatabase mongoDatabase,
    Channel<Message> messageQueue,
    string traceIdentifier,
    CancellationToken cancellationToken = default)
  {
    var valErrors = ValidateCreateContactRequest(request);
    if (ExistErrors(valErrors)) return TypedResults.Problem(JoinErrors(valErrors));

    var contacts = GetContactCollection(mongoDatabase);
    var messages = GetMessageCollection(mongoDatabase);
    var contact = CreateContact(GenerateSequentialGuid(), request.ContactName, request.ContactEmail);

    var (@event, error) = await CreateContactAsync(
      contact,
      (contactName, cancellationToken) => FindContactByName(contacts.AsQueryable(), contactName).FirstOrDefaultAsync(cancellationToken) as Task<Contact?>,
      (contactEmail, cancellationToken) => FindContactByEmail(contacts.AsQueryable(), contactEmail).FirstOrDefaultAsync(cancellationToken) as Task<Contact?>,
      async (contact, message, cancellationToken) =>
      {
        await InsertContactAsync(contacts, contact, cancellationToken);
        await InsertMessageAsync(messages, message, cancellationToken);
      },
      (message) => EnqueueMessage(messageQueue, message),
      traceIdentifier,
      cancellationToken);

    return @event is not null ?
      TypedResults.Created(GetMongoContactPath(@event!.ContactId)) :
      TypedResults.Problem(error, statusCode: 400);
  }
}