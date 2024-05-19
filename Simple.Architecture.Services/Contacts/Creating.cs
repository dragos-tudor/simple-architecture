
namespace Simple.Architecture.Services;

partial class ServicesFuncs
{
  public static async Task<Result<ContactCreatedEvent?, IEnumerable<string>?>> CreateContact (
    Guid contactId,
    string contactName,
    string contactEmail,
    IEnumerable<PhoneNumber>? phoneNumbers,
    Func<IEnumerable<PhoneNumber>, Task<IEnumerable<string>>> FindPhoneNumbers,
    Func<Contact, Message, Task> SaveContactAndMessage,
    Func<Message, Task> PublishMessage)
  {
    var duplicateNumbers = await FindPhoneNumbers(phoneNumbers ?? []);
    if (!IsEmptyCollection(duplicateNumbers)) return GetDuplicatePhoneNumberErrors(duplicateNumbers).ToArray();

    var contact = DomainFuncs.CreateContact(contactId, contactEmail, contactName, phoneNumbers);
    var contactErrors = ValidateContact(contact);
    if (!IsEmptyCollection(contactErrors)) return contactErrors.ToArray();

    var contactCreated = CreateContactCreatedEvent(contact.ContactId, contactEmail);
    var message = CreateMessage(contactCreated);

    await SaveContactAndMessage(contact, message);
    await PublishMessage(message);

    return contactCreated;
  }
}