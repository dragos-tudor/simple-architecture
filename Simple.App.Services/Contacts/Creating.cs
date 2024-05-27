
namespace Simple.App.Services;

partial class ServicesFuncs
{
  public static async Task<Result<ContactCreatedEvent?, string[]?>> CreateContactAppService (
    Contact contact,
    FindModels<PhoneNumber, long> findPhoneNumbers,
    SaveModels<Contact, Message> saveModels,
    PublishModel<Message> publishMessage,
    CancellationToken cancellationToken = default)
  {
    var valErrors = ValidateModel(contact, ContactValidator);
    if(ExistsValidationErrors(valErrors)) return AsArray(valErrors);

    var result = await CreateContactService(contact, findPhoneNumbers, cancellationToken);
    if(IsFailureResult(result)) return AsArray(FromFailure(result)!);

    var @event = FromSuccess(result);
    var message = CreateMessage(@event);

    await saveModels(contact, message, cancellationToken);
    await publishMessage(message);

    return @event;
  }
}