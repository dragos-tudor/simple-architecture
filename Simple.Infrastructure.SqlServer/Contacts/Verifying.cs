
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static bool EqualPhoneNumbers (PhoneNumber phoneNumber1, PhoneNumber phoneNumber2) =>
    GetPhoneNumberKey(phoneNumber1) == GetPhoneNumberKey(phoneNumber2);
}