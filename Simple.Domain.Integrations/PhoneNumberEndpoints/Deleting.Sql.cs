
using Microsoft.EntityFrameworkCore;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<Results<Ok, ProblemHttpResult>> DeletePhoneNumberSqlEndpoint (
    Guid contactId,
    short countryCode,
    long number,
    AgendaContextFactory sqlContextFactory,
    HttpContext httpContext,
    ILogger logger)
  {
    using var agendaContext = await sqlContextFactory.CreateDbContextAsync();
    var result = await DeletePhoneNumberService (
      contactId,
      CreatePhoneNumber(countryCode, number),
      (contactId, cancellationToken) => FindContactByKey(agendaContext.Contacts.AsQueryable(), contactId).Include(c => c.PhoneNumbers).FirstOrDefaultAsync(cancellationToken),
      (contact, phoneNumber, cancellationToken) => DeletePhoneNumberAsync(agendaContext, phoneNumber, cancellationToken),
      logger,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Ok():
      TypedResults.Problem(JoinFailures(FromFailure(result)!));
  }
}