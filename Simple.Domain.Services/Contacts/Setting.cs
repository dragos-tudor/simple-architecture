
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static Guid SetContactId (Contact contact, Guid contactId) => contact.ContactId = contactId;

  public static void SetContactPhoneNumber (Contact contact, PhoneNumber phoneNumber) => contact.PhoneNumbers.Add(phoneNumber);

  public static void SetContactPhoneNumbers (Contact contact, IEnumerable<PhoneNumber> phoneNumbers) { foreach(var phoneNumber in phoneNumbers) SetContactPhoneNumber(contact, phoneNumber); }
}