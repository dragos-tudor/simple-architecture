
using Microsoft.EntityFrameworkCore;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<Results<Created, ProblemHttpResult>> CreateContactSqlAsync(
    CreateContactRequest request,
    AgendaContextFactory dbContextFactory,
    Channel<Message> messageQueue,
    string traceIdentifier,
    CancellationToken cancellationToken = default)
  {
    var valErrors = ValidateCreateContactRequest(request);
    if (ExistErrors(valErrors)) return TypedResults.Problem(JoinErrors(valErrors));

    var dbContext = CreateAgendaContext(dbContextFactory);
    var contact = CreateContact(GenerateSequentialGuid(), request.ContactName, request.ContactEmail);

    var (@event, error) = await CreateContactService(
      contact,
      (contactName, cancellationToken) => FindContactByName(dbContext.Contacts.AsQueryable(), contactName).FirstOrDefaultAsync(cancellationToken),
      (contactEmail, cancellationToken) => FindContactByEmail(dbContext.Contacts.AsQueryable(), contactEmail).FirstOrDefaultAsync(cancellationToken),
      (contact, message, cancellationToken) => InsertContactAndMessageSqlAsync(dbContext, contact, message, cancellationToken),
      (message) => EnqueueMessage(messageQueue, message),
      traceIdentifier,
      cancellationToken);

    return @event is not null ?
      TypedResults.Created(GetSqlContactPath(@event!.ContactId)) :
      TypedResults.Problem(error!, statusCode: 400);
  }
}