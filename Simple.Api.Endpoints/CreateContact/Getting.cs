
namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  internal static string GetMongoContactPath(Guid contactId) => $"/mongo/contacts/{contactId}";

  internal static string GetSqlContactPath(Guid contactId) => $"/sql/contacts/{contactId}";
}