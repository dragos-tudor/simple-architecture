
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<Result<ContactCreatedEvent?, IEnumerable<string>?>> CreateContactService (
    Contact contact,
    FindPhoneNumbers FindPhoneNumbers,
    SaveModelAndMessage<Contact, ContactCreatedEvent> SaveContactAndMessage,
    PublishMessage<ContactCreatedEvent> PublishMessage)
  {
    var duplicateNumbers = await FindPhoneNumbers(contact.PhoneNumbers ?? []);
    if (ExistsPhoneNumbers(duplicateNumbers)) return GetDuplicatePhoneNumberErrors(duplicateNumbers).ToArray();

    var contactErrors = ValidateContact(contact);
    if (ExistValidationErrors(contactErrors)) return contactErrors.ToArray();

    var contactCreated = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    var message = CreateMessage(contactCreated);

    await SaveContactAndMessage(contact, message);
    await PublishMessage(message);

    return contactCreated;
  }
}