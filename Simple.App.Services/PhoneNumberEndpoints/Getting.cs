
namespace Simple.App.Services;

partial class ServicesFuncs
{
  public static string GetMongoPhoneNumbersPath (Guid contactId) => $"/mongo/contacts/{contactId}/phonenumbers";

  public static string GetMongoPhoneNumberCreatedPath (Guid contactId, short countryCode, long number) => $"/mongo/contacts/{contactId}/phonenumbers/{countryCode}/{number}";

  public static string GetSqlPhoneNumbersPath (Guid contactId) => $"/sql/contacts/{contactId}/phonenumbers";

  public static string GetSqlPhoneNumberCreatedPath (Guid contactId, short countryCode, long number) => $"/sql/contacts/{contactId}/phonenumbers/{countryCode}/{number}";
}