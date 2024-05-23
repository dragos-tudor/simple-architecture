
namespace Simple.Web.Api;

partial class ApiFuncs
{
  static void InitializeSqlServer (SqlServerOptions options)
  {
    var (adminName, adminPassword, userName, userPassword, imageName, containerName, databaseName, serverPort) = options;
    var serverIpAddress = StartSqlServer(serverPort, adminPassword, imageName, containerName);
    SetAgendaContextFactory(SqlFuncs.CreateDbContextFactory(CreateAgendaContextOptions(serverIpAddress, databaseName, userName, userPassword)));

    using var masterContext = CreateMasterContext(serverIpAddress, adminName, adminPassword);
    CreateSqlDatabase(masterContext, databaseName);
    CreateSqlDatabaseUser(masterContext, databaseName, userName, userPassword);
    MigrateSqlDatabase(masterContext, databaseName);
  }
}
