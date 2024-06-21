
using MongoDB.Driver;

namespace Simple.App.Services;

partial class ServicesFuncs
{
  public static async Task<Results<Ok, ProblemHttpResult>> DeletePhoneNumberMongoEndpoint (
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
      (contact, phoneNumber, cancellationToken) => DeletePhoneNumber(contacts, contact, phoneNumber, cancellationToken),
      logger,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Ok():
      TypedResults.Problem(JoinFailures(FromFailure(result)!));
  }
}