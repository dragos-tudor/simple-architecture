
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static async Task<Results<Ok, ProblemHttpResult>> AddPhoneNumberSqlEndpoint (
    Guid contactId,
    PhoneNumber phoneNumber,
    AgendaContextFactory agendaContextFactory,
    HttpContext httpContext)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync();
    var result = await AddPhoneNumberService (
      contactId,
      phoneNumber,
      (phoneNumber, cancellationToken) => FindPhoneNumber(agendaContext.PhoneNumbers, phoneNumber).FirstOrDefaultAsync(cancellationToken),
      (contactId, cancellationToken) => FindContactByKey(agendaContext.Contacts, contactId).FirstOrDefaultAsync(cancellationToken),
      (contact, phoneNumber, cancellationToken) => InsertPhoneNumber(agendaContext, contact, phoneNumber, cancellationToken),
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Ok():
      TypedResults.Problem(JoinFailures(FromFailure(result)!));
  }
}