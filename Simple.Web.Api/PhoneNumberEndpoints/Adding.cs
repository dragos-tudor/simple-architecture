
namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static async Task<Results<Ok, BadRequest<string[]>>> AddPhoneNumberEndpoint (
    Guid contactId,
    PhoneNumber phoneNumber,
    FindModel<PhoneNumber, PhoneNumber?> findPhoneNumber,
    FindModel<Guid, Contact?> findContact,
    SaveModels<Contact, PhoneNumber> saveModels,
    HttpContext httpContext)
  {
    var result = await AddPhoneNumberService (
      contactId,
      phoneNumber,
      findPhoneNumber,
      findContact,
      saveModels,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Ok():
      TypedResults.BadRequest(FromFailure(result)!);
  }
}