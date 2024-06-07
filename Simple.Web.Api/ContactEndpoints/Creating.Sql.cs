#pragma warning disable CA2234

using Microsoft.EntityFrameworkCore;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static async Task<Results<Created, ProblemHttpResult>> CreateContactSqlEndpoint (
    Contact contact,
    AgendaContextFactory agendaContextFactory,
    Channel<Message> messageQueue,
    HttpContext httpContext)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync();
    var result = await CreateContactService (
      contact,
      (phoneNumbers, cancellationToken) => FindPhoneNumbers(agendaContext.PhoneNumbers, phoneNumbers, cancellationToken),
      (contactName, cancellationToken) => FindContactByName(agendaContext.Contacts, contactName).FirstOrDefaultAsync(cancellationToken),
      (contactEmail, cancellationToken) => FindContactByEmail(agendaContext.Contacts, contactEmail).FirstOrDefaultAsync(cancellationToken),
      (contact, message, cancellationToken) => InsertContactAndMessage(agendaContext, contact, message, cancellationToken),
      (message) => ProduceMessage(messageQueue, message),
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Created(GetContactCreatedUri(httpContext.Request, FromSuccess(result)!)):
      TypedResults.Problem(JoinErrors(FromFailure(result)!), statusCode: 400);
  }
}