

using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static async Task<Results<Created, BadRequest<string[]>>> CreateContactSqlEndpoint (
    Contact contact,
    IEnumerable<PhoneNumber> phoneNumbers,
    AgendaContextFactory agendaContextFactory,
    Channel<Message> messageQueue,
    HttpContext httpContext)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync();
    var result = await CreateContactApi (
      contact,
      phoneNumbers,
      (phoneNumbers, cancellationToken) => FindPhoneNumbers(agendaContext.PhoneNumbers, phoneNumbers, cancellationToken),
      (contact, message, cancellationToken) => InsertContactAndMessage(agendaContext, contact, message, cancellationToken),
      (message) => ProduceMessage(messageQueue, message),
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Created(GetContactCreatedUri(FromSuccess(result)!)):
      TypedResults.BadRequest(FromFailure(result)!);
  }

  public static async Task<Results<Created, BadRequest<string[]>>> CreateContactMongoEndpoint (
    Contact contact,
    IEnumerable<PhoneNumber> phoneNumbers,
    IMongoDatabase agendaDb,
    Channel<Message> messageQueue,
    HttpContext httpContext)
  {
    var contacts = GetContactCollection(agendaDb);
    var messages = GetMessageCollection(agendaDb);
    var result = await CreateContactApi (
      contact,
      phoneNumbers,
      (phoneNumbers, cancellationToken) => FindPhoneNumbers(contacts.AsQueryable(), phoneNumbers, cancellationToken),
      (contact, message, cancellationToken) => InsertContactAndMessage(contacts, messages, contact, message, default, cancellationToken),
      (message) => ProduceMessage(messageQueue, message),
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Created(GetContactCreatedUri(FromSuccess(result)!)):
      TypedResults.BadRequest(FromFailure(result)!);
  }
}