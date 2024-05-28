
namespace Simple.App.Services;

partial class ServicesFuncs
{
  public static async Task<Result<Contact?, string[]?>> CreateContactService (
    Contact contact,
    IEnumerable<PhoneNumber> phoneNumbers,
    FindModels<PhoneNumber, long> findPhoneNumbers,
    SaveModels<Contact, Message> saveModels,
    ProduceMessage<Message> produceMessage,
    CancellationToken cancellationToken = default)
  {
    var contactErrors = ValidateModel(contact, ContactValidator);
    if(ExistsValidationErrors(contactErrors)) return AsArray(contactErrors);

    var phoneNumberErrors = ValidateModels(phoneNumbers, PhoneNumberValidator);
    if(ExistsValidationErrors(phoneNumberErrors)) return AsArray(phoneNumberErrors);

    var result = await DomainFuncs.CreateContactService(contact, phoneNumbers, findPhoneNumbers, cancellationToken);
    if(IsFailureResult(result)) return AsArray(FromFailure(result)!);

    var @event = FromSuccess(result);
    var message = CreateMessage(@event);

    await saveModels(contact, message, cancellationToken);
    await produceMessage(message);

    return contact;
  }
}