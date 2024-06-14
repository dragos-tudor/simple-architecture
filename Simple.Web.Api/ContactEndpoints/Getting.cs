
namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static string GetMongoContactCreatedPath (Guid contactId) => $"/mongo/contacts/{contactId}";

  internal static string GetSqlContactCreatedPath (Guid contactId) => $"/sql/contacts/{contactId}";
}