using SqlFuncs = Storing.SqlServer.SqlServerFuncs;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static string CreateDbConnectionString (
    string dbName,
    string serverName,
    string userName,
    string password)
  =>
    SqlFuncs.CreateSqlConnection(
      dbName, userName, password, serverName,
      builder => {
        builder.ConnectTimeout = 60;
        builder.TrustServerCertificate = true;
      }
    );
}