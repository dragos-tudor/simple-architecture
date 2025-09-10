
namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static string GetPhoneNumberMongoPath(Guid contactId, short countryCode, long number) => $"/mongo/contacts/{contactId}/phonenumbers/{countryCode}/{number}";

  public static string GetPhoneNumberSqlPath(Guid contactId, short countryCode, long number) => $"/sql/contacts/{contactId}/phonenumbers/{countryCode}/{number}";
}