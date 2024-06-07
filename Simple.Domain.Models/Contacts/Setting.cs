
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static void SetContactPhoneNumber (Contact contact, PhoneNumber phoneNumber) => contact.PhoneNumbers.Add(phoneNumber);

  public static void SetContactPhoneNumbers (Contact contact, IEnumerable<PhoneNumber> phoneNumbers) { foreach(var phoneNumber in phoneNumbers) SetContactPhoneNumber(contact, phoneNumber); }
}