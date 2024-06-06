

namespace Simple.Domain.Api;

partial class ApiFuncs
{
  public static async Task<Result<Contact?, string[]?>> CreateContactApi (
    Contact contact,
    FindModels<PhoneNumber, PhoneNumber> findPhoneNumbers,
    SaveModels<Contact, Message<ContactCreatedEvent>> saveContactAndMessage,
    ProduceMessage<Message<ContactCreatedEvent>> produceMessage,
    string? traceId = default,
    CancellationToken cancellationToken = default)
  {
    contact = EnsureContactPhoneNumbers(contact);

    var contactErrors = ValidateObject(contact, ContactValidator);
    if(ExistsValidationErrors(contactErrors)) return AsArray(contactErrors);

    var phoneNumberErrors = ValidateObjects(contact.PhoneNumbers, PhoneNumberValidator);
    if(ExistsValidationErrors(phoneNumberErrors)) return AsArray(phoneNumberErrors);

    var result = await CreateContactService(contact, findPhoneNumbers, cancellationToken);
    if(IsFailureResult(result)) return AsArray(FromFailure(result)!);

    var @event = FromSuccess(result)!;
    var message = CreateMessage(@event, traceId: traceId);

    await saveContactAndMessage(contact, message, cancellationToken);
    LogContactCreated(Logger, contact.ContactId, traceId);

    produceMessage(message);
    return contact;
  }
}