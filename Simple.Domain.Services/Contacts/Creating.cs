
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<Result<ContactCreatedEvent?, IEnumerable<string>?>> CreateContactService (
    Contact contact,
    FindPhoneNumbers FindPhoneNumbers,
    SaveModelAndEvent<Contact, ContactCreatedEvent> SaveContactAndEvent,
    PublishEvent<ContactCreatedEvent> PublishEvent)
  {
    var duplicateNumbers = await FindPhoneNumbers(contact.PhoneNumbers ?? []);
    if (ExistsPhoneNumbers(duplicateNumbers)) return GetDuplicatePhoneNumberErrors(duplicateNumbers).ToArray();

    var contactErrors = ValidateContact(contact);
    if (ExistValidationErrors(contactErrors)) return contactErrors.ToArray();

    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);

    await SaveContactAndEvent(contact, contactCreatedEvent);
    await PublishEvent(contactCreatedEvent);

    return contactCreatedEvent;
  }
}