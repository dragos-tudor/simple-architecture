
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static async Task<Results<Ok, BadRequest<string[]>>> AddPhoneNumberSqlEndpoint (
    Guid contactId,
    PhoneNumber phoneNumber,
    Func<AgendaContext> createAgendaContext,
    ProduceMessage<Message> produceMessage,
    HttpContext httpContext)
  {
    using var agendaContext = createAgendaContext();
    var result = await AddPhoneNumberApi (
      contactId,
      phoneNumber,
      (phoneNumber, cancellationToken) => FindPhoneNumber(agendaContext.PhoneNumbers, phoneNumber).FirstOrDefaultAsync(cancellationToken),
      (contactId, cancellationToken) => FindContactByKey(agendaContext.Contacts, contactId).FirstOrDefaultAsync(cancellationToken),
      (contact, phoneNumber, cancellationToken) => InsertPhoneNumber(agendaContext, contact, phoneNumber, cancellationToken),
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Ok():
      TypedResults.BadRequest(FromFailure(result)!);
  }

  public static async Task<Results<Ok, BadRequest<string[]>>> AddPhoneNumberMongoEndpoint (
    Guid contactId,
    PhoneNumber phoneNumber,
    IMongoCollection<Contact> contacts,
    ProduceMessage<Message> produceMessage,
    HttpContext httpContext)
  {
    var result = await AddPhoneNumberApi (
      contactId,
      phoneNumber,
      (phoneNumber, cancellationToken) => FindPhoneNumber(contacts.AsQueryable(), phoneNumber, cancellationToken),
      (contactId, cancellationToken) => (FindContactByKey(contacts.AsQueryable(), contactId) as IQueryable<Contact>).FirstOrDefaultAsync(cancellationToken),
      (contact, phoneNumber, cancellationToken) => InsertPhoneNumber(contacts, contact, phoneNumber, cancellationToken),
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Ok():
      TypedResults.BadRequest(FromFailure(result)!);
  }
}