#pragma warning disable CA2234

using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static async Task<Results<Created, BadRequest<string[]>>> CreateContactMongoEndpoint (
    Contact contact,
    IMongoDatabase agendaDb,
    Channel<Message> messageQueue,
    HttpContext httpContext)
  {
    var contacts = GetContactCollection(agendaDb);
    var messages = GetMessageCollection(agendaDb);
    var result = await CreateContactApi (
      contact,
      (phoneNumbers, cancellationToken) => FindPhoneNumbers(contacts.AsQueryable(), phoneNumbers, cancellationToken),
      (contact, message, cancellationToken) => InsertContactAndMessage(contacts, messages, contact, message, default, cancellationToken),
      (message) => ProduceMessage(messageQueue, message),
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Created(GetContactCreatedUri(httpContext.Request, FromSuccess(result)!)):
      TypedResults.BadRequest(FromFailure(result)!);
  }
}