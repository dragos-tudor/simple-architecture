
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static void SetContactPhoneNumber (Contact contact, PhoneNumber phoneNumber) =>
    contact.PhoneNumbers = [.. contact.PhoneNumbers ?? [], phoneNumber];

  static void SetContactPhoneNumbers (Contact contact, PhoneNumber[] phoneNumbers) =>
    contact.PhoneNumbers = phoneNumbers;
}