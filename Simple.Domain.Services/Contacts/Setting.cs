
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static Guid SetContactId (Contact contact, Guid contactId) => IsMissingContactId(contact.ContactId)? contact.ContactId = contactId: contact.ContactId;

  public static void SetContactPhoneNumber (Contact contact, PhoneNumber phoneNumber) => contact.PhoneNumbers.Add(phoneNumber);
}