
using Microsoft.EntityFrameworkCore;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static async Task<Results<Ok, BadRequest<string[]>>> AddPhoneNumberEndpoint (
    Guid contactId,
    PhoneNumber phoneNumber,
    Func<AgendaContext> createAgendaContext,
    ProduceMessage<Message> produceMessage,
    HttpContext httpContext)
  {
    using var agendaContext = createAgendaContext();
    var result = await AddPhoneNumberService (
      contactId,
      phoneNumber,
      (phoneNumber, cancellationToken) => FindPhoneNumber(agendaContext.PhoneNumbers, phoneNumber).FirstOrDefaultAsync(cancellationToken),
      (contactId, cancellationToken) => FindContactById(agendaContext.Contacts, contactId).FirstOrDefaultAsync(cancellationToken),
      (contact, phoneNumber, cancellationToken) => InsertContactAndPhoneNumber(agendaContext, contact, phoneNumber, cancellationToken),
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Ok():
      TypedResults.BadRequest(FromFailure(result)!);
  }
}