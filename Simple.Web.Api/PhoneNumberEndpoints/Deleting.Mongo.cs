
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static async Task<Results<Ok, ProblemHttpResult>> DeletePhoneNumberMongoEndpoint (
    Guid contactId,
    short countryCode,
    long number,
    IMongoDatabase agendaDb,
    HttpContext httpContext,
    ILogger logger)
  {
    var contacts = GetContactCollection(agendaDb);
    var result = await DeletePhoneNumberService (
      contactId,
      CreatePhoneNumber(countryCode, number),
      (contactId, cancellationToken) => FindContactByKey(contacts.AsQueryable(), contactId).FirstOrDefaultAsync(cancellationToken) as Task<Contact?>,
      (contact, phoneNumber, cancellationToken) => DeleteContactPhoneNumber(contacts, contact, phoneNumber, cancellationToken),
      logger,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Ok():
      TypedResults.Problem(JoinFailures(FromFailure(result)!));
  }
}