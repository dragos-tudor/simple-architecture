
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  const string ContainerName = "simple-sql";
  const string ImageName = "mcr.microsoft.com/mssql/server:2019-latest";

  const int ServerPort = 1433;
  static string ServerIpAddress = string.Empty;

  static void InitializeSqlServer ()
  {
    var networkSettings = StartSqlContainer(ServerPort, AdminPassword, ImageName, ContainerName);
    ServerIpAddress = GetServerIpAddress(networkSettings);

    using var context = CreateAgendaContext();
    MigrateDatabase(context);
  }
}