
using System.Threading;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<Result<ContactCreatedEvent?, string[]?>> CreateContactService (
    Contact contact,
    FindModels<PhoneNumber, long> findPhoneNumbers,
    SaveModels<Contact, Message<ContactCreatedEvent>> saveContactAndMessage,
    PublishModel<Message<ContactCreatedEvent>> publishMessage,
    CancellationToken cancellationToken = default)
  {
    var duplicateNumbers = await findPhoneNumbers(contact.PhoneNumbers ?? [], cancellationToken);
    if (ExistsPhoneNumbers(duplicateNumbers)) return AsArray(GetDuplicatePhoneNumberErrors(duplicateNumbers));

    var contactErrors = ValidateContact(contact);
    if (ExistsValidationErrors(contactErrors)) return AsArray(contactErrors);

    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    var message = CreateMessage(contactCreatedEvent);

    await saveContactAndMessage(contact, message, cancellationToken);
    await publishMessage(message);

    return contactCreatedEvent;
  }
}