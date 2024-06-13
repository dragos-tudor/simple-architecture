#pragma warning disable CA2234

using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static async Task<Results<Created, ProblemHttpResult>> AddPhoneNumberMongoEndpoint (
    Guid contactId,
    PhoneNumber phoneNumber,
    IMongoDatabase agendaDb,
    HttpContext httpContext,
    ILogger logger)
  {
    var contacts = GetContactCollection(agendaDb);
    var result = await AddPhoneNumberService (
      contactId,
      phoneNumber,
      (phoneNumber, cancellationToken) => FindPhoneNumber(contacts.AsQueryable(), phoneNumber, cancellationToken),
      (contactId, cancellationToken) => FindContactByKey(contacts.AsQueryable(), contactId).FirstOrDefaultAsync(cancellationToken) as Task<Contact?>,
      (contact, phoneNumber, cancellationToken) => InsertPhoneNumber(contacts, contact, phoneNumber, cancellationToken),
      logger,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Created(GetPhoneNumberCreatedUri(httpContext.Request, phoneNumber)):
      TypedResults.Problem(JoinFailures(FromFailure(result)!));
  }
}