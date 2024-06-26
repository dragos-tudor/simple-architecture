#pragma warning disable CA2234

using Microsoft.EntityFrameworkCore;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<Results<Created, ProblemHttpResult>> AddPhoneNumberSqlEndpoint (
    Guid contactId,
    PhoneNumber phoneNumber,
    AgendaContextFactory sqlContextFactory,
    HttpContext httpContext,
    ILogger logger)
  {
    using var agendaContext = await sqlContextFactory.CreateDbContextAsync();
    var result = await AddPhoneNumberService (
      contactId,
      phoneNumber,
      (phoneNumber, cancellationToken) => FindPhoneNumber(agendaContext.PhoneNumbers.AsQueryable(), phoneNumber).FirstOrDefaultAsync(cancellationToken),
      (contactId, cancellationToken) => FindContactByKey(agendaContext.Contacts.AsQueryable(), contactId).FirstOrDefaultAsync(cancellationToken),
      (contact, phoneNumber, cancellationToken) => InsertPhoneNumberAsync(agendaContext, contact, phoneNumber, cancellationToken),
      logger,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Created(GetSqlPhoneNumberCreatedPath(contactId, phoneNumber.CountryCode, phoneNumber.Number)):
      TypedResults.Problem(JoinFailures(FromFailure(result)!));
  }
}