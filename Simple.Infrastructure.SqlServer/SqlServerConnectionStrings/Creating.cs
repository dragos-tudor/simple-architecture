
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static string CreateSqlServerConnectionString (SqlServerOptions sqlServerOptions) => CreateSqlConnectionString(sqlServerOptions.DbName, sqlServerOptions.UserName, sqlServerOptions.UserPassword, sqlServerOptions.ContainerName);
}