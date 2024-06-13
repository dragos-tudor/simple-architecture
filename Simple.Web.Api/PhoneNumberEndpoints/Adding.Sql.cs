#pragma warning disable CA2234

using Microsoft.EntityFrameworkCore;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static async Task<Results<Created, ProblemHttpResult>> AddPhoneNumberSqlEndpoint (
    Guid contactId,
    PhoneNumber phoneNumber,
    AgendaContextFactory agendaContextFactory,
    HttpContext httpContext,
    ILogger logger)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync();
    var result = await AddPhoneNumberService (
      contactId,
      phoneNumber,
      (phoneNumber, cancellationToken) => FindPhoneNumber(agendaContext.PhoneNumbers.AsQueryable(), phoneNumber).FirstOrDefaultAsync(cancellationToken),
      (contactId, cancellationToken) => FindContactByKey(agendaContext.Contacts.AsQueryable(), contactId).FirstOrDefaultAsync(cancellationToken),
      (contact, phoneNumber, cancellationToken) => InsertPhoneNumber(agendaContext, contact, phoneNumber, cancellationToken),
      logger,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Created(GetPhoneNumberCreatedUri(httpContext.Request, phoneNumber)):
      TypedResults.Problem(JoinFailures(FromFailure(result)!));
  }
}