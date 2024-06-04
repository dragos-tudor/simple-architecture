
namespace Simple.Domain.Api;

partial class ApiFuncs
{
  public static async Task<Result<Contact?, string[]?>> CreateContactApi (
    Contact contact,
    IEnumerable<PhoneNumber> phoneNumbers,
    FindModels<PhoneNumber, PhoneNumber> findPhoneNumbers,
    SaveModelAndMessage<Contact, ContactCreatedEvent> saveContactAndMessage,
    ProduceMessage<ContactCreatedEvent> produceMessage,
    string? traceId = default,
    CancellationToken cancellationToken = default)
  {
    var contactErrors = ValidateObject(contact, ContactValidator);
    if(ExistsValidationErrors(contactErrors)) return AsArray(contactErrors);

    var phoneNumberErrors = ValidateObjects(phoneNumbers, PhoneNumberValidator);
    if(ExistsValidationErrors(phoneNumberErrors)) return AsArray(phoneNumberErrors);

    var result = await CreateContactService(contact, phoneNumbers, findPhoneNumbers, cancellationToken);
    if(IsFailureResult(result)) return AsArray(FromFailure(result)!);

    var @event = FromSuccess(result)!;
    var message = CreateMessage(@event, traceId: traceId);

    await saveContactAndMessage(contact, message, cancellationToken);
    await produceMessage(message);

    LogContactCreated(Logger, contact.ContactId, traceId);
    return contact;
  }
}