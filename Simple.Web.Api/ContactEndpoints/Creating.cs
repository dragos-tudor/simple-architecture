
namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static async Task<Results<Created, BadRequest<string[]>>> CreateContactEndpoint (
    Contact contact,
    IEnumerable<PhoneNumber> phoneNumbers,
    Func<AgendaContext> createAgendaContext,
    ProduceMessage<Message> produceMessage,
    HttpContext httpContext)
  {
    using var agendaContext = createAgendaContext();
    var result = await CreateContactService (
      contact,
      phoneNumbers,
      (phoneNumbers, cancellationToken) => FindPhoneNumbersWithPhoneNumbers(agendaContext.PhoneNumbers, phoneNumbers, cancellationToken),
      (contact, message, cancellationToken) => InsertContactAndMessage(agendaContext, contact, message, cancellationToken),
      produceMessage,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Created(GetContactCreatedUri(FromSuccess(result)!)):
      TypedResults.BadRequest(FromFailure(result)!);
  }
}