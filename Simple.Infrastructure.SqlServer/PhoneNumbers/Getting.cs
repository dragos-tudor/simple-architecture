
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static PhoneNumber? GetPhoneNumber (IEnumerable<PhoneNumber> phoneNumbers, PhoneNumber phoneNumber) =>
    phoneNumbers.Where(item => GetPhoneNumberKey(phoneNumber) == GetPhoneNumberKey(item)).FirstOrDefault();

  public static (string CountryCode, string Number) GetPhoneNumberKey (PhoneNumber phoneNumber) =>
    (phoneNumber.CountryCode, phoneNumber.Number);
}