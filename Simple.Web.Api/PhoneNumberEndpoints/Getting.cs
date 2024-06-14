
namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static string GetMongoPhoneNumbersPath (Guid contactId) => $"/mongo/contacts/{contactId}/phonenumbers";

  internal static string GetMongoPhoneNumberCreatedPath (Guid contactId, short countryCode, long number) => $"/mongo/contacts/{contactId}/phonenumbers/{countryCode}/{number}";

  internal static string GetSqlPhoneNumbersPath (Guid contactId) => $"/sql/contacts/{contactId}/phonenumbers";

  internal static string GetSqlPhoneNumberCreatedPath (Guid contactId, short countryCode, long number) => $"/sql/contacts/{contactId}/phonenumbers/{countryCode}/{number}";
}