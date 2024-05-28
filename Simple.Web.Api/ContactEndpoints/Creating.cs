
namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static async Task<Results<Created, BadRequest<string[]>>> CreateContactEndpoint (
    Contact contact,
    IEnumerable<PhoneNumber> phoneNumbers,
    FindModels<PhoneNumber, PhoneNumber> findPhoneNumbers,
    SaveModels<Contact, Message> saveModels,
    ProduceMessage<Message> produceMessage,
    HttpContext httpContext)
  {
    var result = await CreateContactService (
      contact,
      phoneNumbers,
      findPhoneNumbers,
      saveModels,
      produceMessage,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Created(GetContactCreatedUri(FromSuccess(result)!)):
      TypedResults.BadRequest(FromFailure(result)!);
  }
}