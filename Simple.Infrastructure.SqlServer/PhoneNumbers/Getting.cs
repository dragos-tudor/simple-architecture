
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static (string CountryCode, string Number) GetPhoneNumberKey (PhoneNumber phoneNumber) =>
    (phoneNumber.CountryCode, phoneNumber.Number);
}