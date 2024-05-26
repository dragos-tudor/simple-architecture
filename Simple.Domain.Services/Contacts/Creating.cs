
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<Result<ContactCreatedEvent?, string[]?>> CreateContactService (
    Contact contact,
    FindModels<PhoneNumber, long> FindPhoneNumbers,
    SaveModelAndEvent<Contact, ContactCreatedEvent> SaveContactAndEvent,
    PublishEvent<ContactCreatedEvent> PublishEvent)
  {
    var duplicateNumbers = await FindPhoneNumbers(contact.PhoneNumbers ?? []);
    if (ExistsPhoneNumbers(duplicateNumbers)) return AsArray(GetDuplicatePhoneNumberErrors(duplicateNumbers));

    var contactErrors = ValidateContact(contact);
    if (ExistValidationErrors(contactErrors)) return AsArray(contactErrors);

    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);

    await SaveContactAndEvent(contact, contactCreatedEvent);
    await PublishEvent(contactCreatedEvent);

    return contactCreatedEvent;
  }
}