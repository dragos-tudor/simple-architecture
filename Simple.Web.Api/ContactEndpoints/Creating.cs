
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static async Task<Results<Created, BadRequest<string[]>>> CreateContactSqlEndpoint (
    Contact contact,
    IEnumerable<PhoneNumber> phoneNumbers,
    Func<AgendaContext> createAgendaContext,
    ProduceMessage<Message> produceMessage,
    HttpContext httpContext)
  {
    using var agendaContext = createAgendaContext();
    var result = await CreateContactApi (
      contact,
      phoneNumbers,
      (phoneNumbers, cancellationToken) => FindPhoneNumbers(agendaContext.PhoneNumbers, phoneNumbers, cancellationToken),
      (contact, message, cancellationToken) => InsertContactAndMessage(agendaContext, contact, message, cancellationToken),
      produceMessage,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Created(GetContactCreatedUri(FromSuccess(result)!)):
      TypedResults.BadRequest(FromFailure(result)!);
  }

  public static async Task<Results<Created, BadRequest<string[]>>> CreateContactMongoEndpoint (
    Contact contact,
    IEnumerable<PhoneNumber> phoneNumbers,
    IMongoCollection<Contact> contacts,
    IMongoCollection<Message> messages,
    ProduceMessage<Message> produceMessage,
    HttpContext httpContext)
  {
    var result = await CreateContactApi (
      contact,
      phoneNumbers,
      (phoneNumbers, cancellationToken) => FindPhoneNumbers(contacts.AsQueryable(), phoneNumbers, cancellationToken),
      (contact, message, cancellationToken) => InsertContactAndMessage(contacts, messages, contact, message, default, cancellationToken),
      produceMessage,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Created(GetContactCreatedUri(FromSuccess(result)!)):
      TypedResults.BadRequest(FromFailure(result)!);
  }
}