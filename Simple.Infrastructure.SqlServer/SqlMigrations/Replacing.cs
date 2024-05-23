
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static string ReplaceSqlMigrationToken (string script, string token, string replaceValue) =>
    script.Replace(token, replaceValue, StringComparison.InvariantCulture);
}