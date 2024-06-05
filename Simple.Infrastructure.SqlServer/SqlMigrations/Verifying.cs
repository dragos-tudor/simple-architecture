
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static bool IsSqlMigrationName (string fileName) => fileName.EndsWith(".sql", StringComparison.InvariantCulture);
}