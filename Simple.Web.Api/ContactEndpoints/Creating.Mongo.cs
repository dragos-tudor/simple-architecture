#pragma warning disable CA2234

using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static async Task<Results<Created, ProblemHttpResult>> CreateContactMongoEndpoint (
    Contact contact,
    IMongoDatabase agendaDb,
    Channel<Message> messageQueue,
    HttpContext httpContext,
    ILogger logger)
  {
    var contacts = GetContactCollection(agendaDb);
    var messages = GetMessageCollection(agendaDb);
    SetContactId(contact, GenerateSequentialGuid());

    var result = await CreateContactService (
      contact,
      (phoneNumbers, cancellationToken) => FindPhoneNumbers(contacts.AsQueryable(), phoneNumbers, cancellationToken),
      (contactName, cancellationToken) => FindContactByName(contacts.AsQueryable(), contactName).FirstOrDefaultAsync(cancellationToken) as Task<Contact?>,
      (contactEmail, cancellationToken) => FindContactByEmail(contacts.AsQueryable(), contactEmail).FirstOrDefaultAsync(cancellationToken) as Task<Contact?>,
      (contact, message, cancellationToken) => InsertContactAndMessage(contacts, messages, contact, message, default, cancellationToken),
      (message) => ProduceMessage(messageQueue, message),
      logger,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Created(GetMongoContactCreatedPath(FromSuccess(result)!.ContactId)):
      TypedResults.Problem(JoinFailures(FromFailure(result)!), statusCode: 400);
  }
}