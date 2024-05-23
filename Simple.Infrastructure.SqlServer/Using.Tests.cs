global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using SqlFuncs = Storing.SqlServer.SqlServerFuncs;

namespace Simple.Infrastructure.SqlServer;

[TestClass]
public partial class SqlServerTests
{
  [AssemblyInitialize]
  public static void InitializeSqlServer(TestContext _)
  {
    const string adminName = "sa";
    const string adminPassword = "admin.P@ssw0rd";
    const string userName = "sqluser";
    const string userPassword = "sqluser.P@ssw0rd";
    const string imageName = "mcr.microsoft.com/mssql/server:2019-latest";
    const string containerName = "simple-sql";
    const string databaseName = "agenda";
    const int serverPort = 1433;

    var serverIpAddress = StartSqlServer(serverPort, adminPassword, imageName, containerName);
    SetAgendaContextFactory(SqlFuncs.CreateDbContextFactory(CreateAgendaContextOptions(serverIpAddress, databaseName, userName, userPassword)));

    using var masterContext = CreateMasterContext(serverIpAddress, adminName, adminPassword);
    CreateSqlDatabase(masterContext, databaseName);
    CreateSqlDatabaseUser(masterContext, databaseName, userName, userPassword);
    MigrateSqlDatabase(masterContext, databaseName);

    using var agendaContext = CreateAgendaContext();
    CleanAgendaDatabase(agendaContext);
  }
}