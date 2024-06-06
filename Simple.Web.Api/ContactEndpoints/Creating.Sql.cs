#pragma warning disable CA2234

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static async Task<Results<Created, BadRequest<string>>> CreateContactSqlEndpoint (
    Contact contact,
    AgendaContextFactory agendaContextFactory,
    Channel<Message> messageQueue,
    HttpContext httpContext)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync();
    var result = await CreateContactApi (
      contact,
      (phoneNumbers, cancellationToken) => FindPhoneNumbers(agendaContext.PhoneNumbers, phoneNumbers, cancellationToken),
      (contact, message, cancellationToken) => InsertContactAndMessage(agendaContext, contact, message, cancellationToken),
      (message) => ProduceMessage(messageQueue, message),
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Created(GetContactCreatedUri(httpContext.Request, FromSuccess(result)!)):
      TypedResults.BadRequest(JoinValidationErrors(FromFailure(result)!));
  }
}