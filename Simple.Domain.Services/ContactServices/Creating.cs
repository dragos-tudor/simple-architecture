
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<Result<ContactCreatedEvent?, IEnumerable<string>?>> CreateContactService (
    Guid contactId,
    string contactName,
    string contactEmail,
    IList<PhoneNumber>? phoneNumbers,
    Func<IEnumerable<PhoneNumber>, Task<IEnumerable<string>>> FindPhoneNumbers,
    Func<Contact, Message<ContactCreatedEvent>, Task> SaveContactAndMessage,
    Func<Message<ContactCreatedEvent>, Task> PublishMessage)
  {
    var duplicateNumbers = await FindPhoneNumbers(phoneNumbers ?? []);
    if (ExistsPhoneNumbers(duplicateNumbers)) return GetDuplicatePhoneNumberErrors(duplicateNumbers).ToArray();

    var contact = CreateContact(contactId, contactEmail, contactName, phoneNumbers);
    var contactErrors = ValidateContact(contact);
    if (ExistValidationErrors(contactErrors)) return contactErrors.ToArray();

    var contactCreated = CreateContactCreatedEvent(contact.ContactId, contactEmail);
    var message = CreateMessage(contactCreated);

    await SaveContactAndMessage(contact, message);
    await PublishMessage(message);

    return contactCreated;
  }
}