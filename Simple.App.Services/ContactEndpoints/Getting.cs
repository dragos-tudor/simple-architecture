
namespace Simple.App.Services;

partial class ServicesFuncs
{
  internal static string GetMongoContactCreatedPath (Guid contactId) => $"/mongo/contacts/{contactId}";

  internal static string GetSqlContactCreatedPath (Guid contactId) => $"/sql/contacts/{contactId}";
}