
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static string CreateSqlConnectionString(SqlServerOptions options) =>
    Storing.SqlServer.SqlServerFuncs.CreateSqlConnectionString(options.DbName, options.UserName, options.UserPassword, options.ServerName);
}