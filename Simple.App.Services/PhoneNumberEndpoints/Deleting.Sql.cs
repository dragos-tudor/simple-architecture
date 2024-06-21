
using Microsoft.EntityFrameworkCore;

namespace Simple.App.Services;

partial class ServicesFuncs
{
  public static async Task<Results<Ok, ProblemHttpResult>> DeletePhoneNumberSqlEndpoint (
    Guid contactId,
    short countryCode,
    long number,
    AgendaContextFactory agendaContextFactory,
    HttpContext httpContext,
    ILogger logger)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync();
    var result = await DeletePhoneNumberService (
      contactId,
      CreatePhoneNumber(countryCode, number),
      (contactId, cancellationToken) => FindContactByKey(agendaContext.Contacts.AsQueryable(), contactId).Include(c => c.PhoneNumbers).FirstOrDefaultAsync(cancellationToken),
      (contact, phoneNumber, cancellationToken) => DeletePhoneNumber(agendaContext, phoneNumber, cancellationToken),
      logger,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Ok():
      TypedResults.Problem(JoinFailures(FromFailure(result)!));
  }
}