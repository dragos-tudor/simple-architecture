
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static string CreateSqlConnectionString (string dbName, string serverName, string userName, string password) =>
    Storing.SqlServer.SqlServerFuncs.CreateSqlConnectionString (
      dbName, userName, password, serverName,
      builder => {
        builder.ConnectTimeout = 60;
        builder.TrustServerCertificate = true;
      }
    );
}