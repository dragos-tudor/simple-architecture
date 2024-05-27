
namespace Simple.Web.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<Results<Created, BadRequest<string>>> CreateContactEndpoint (
    Contact contact,
    string traceId,
    FindModels<PhoneNumber, long> findPhoneNumbers,
    SaveModels<Contact, Message> saveModels,
    PublishMessage<Message> publishMessage,
    CancellationToken cancellationToken = default)
  {
    SetContactId(contact, GenerateContactId());

    var valErrors = ValidateModel(contact, ContactValidator);
    if(ExistsValidationErrors(valErrors)) return TypedResults.BadRequest(JoinValidationErrors(valErrors));

    var result = await CreateContactService(
      contact,
      findPhoneNumbers,
      (contact, message, cancellationToken) => saveModels(contact, SetMessageTraceId(message, traceId), cancellationToken),
      (message) => publishMessage(message),
      cancellationToken);

    return IsSuccessResult(result)?
      TypedResults.Created(GetContactCreatedUri(contact)):
      TypedResults.BadRequest(JoinValidationErrors(FromFailure(result)!));
  }
}