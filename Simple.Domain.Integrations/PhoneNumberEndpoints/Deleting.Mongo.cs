
using MongoDB.Driver;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<Results<Ok, ProblemHttpResult>> DeletePhoneNumberMongoEndpoint (
    Guid contactId,
    short countryCode,
    long number,
    IMongoDatabase mongoDatabase,
    HttpContext httpContext,
    ILogger logger)
  {
    var contacts = GetContactCollection(mongoDatabase);
    var result = await DeletePhoneNumberService (
      contactId,
      CreatePhoneNumber(countryCode, number),
      (contactId, cancellationToken) => FindContactByKey(contacts.AsQueryable(), contactId).FirstOrDefaultAsync(cancellationToken) as Task<Contact?>,
      (contact, phoneNumber, cancellationToken) => DeletePhoneNumberAsync(contacts, contact, phoneNumber, cancellationToken),
      logger,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Ok():
      TypedResults.Problem(JoinFailures(FromFailure(result)!));
  }
}