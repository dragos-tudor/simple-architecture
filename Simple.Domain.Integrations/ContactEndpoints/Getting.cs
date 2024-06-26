
namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  internal static string GetMongoContactCreatedPath (Guid contactId) => $"/mongo/contacts/{contactId}";

  internal static string GetSqlContactCreatedPath (Guid contactId) => $"/sql/contacts/{contactId}";
}