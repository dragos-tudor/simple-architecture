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
    const string agendaDatabaseName = "agenda";
    const int serverPort = 1433;

    var serverIpAddress = StartSqlServer(serverPort, adminPassword, imageName, containerName);
    AgendaContextFactory = SqlFuncs.CreateDbContextFactory(CreateAgendaContextOptions(serverIpAddress, agendaDatabaseName, userName, userPassword));

    using var masterContext = CreateMasterContext(serverIpAddress, adminName, adminPassword);
    CreateSqlDatabase(masterContext, agendaDatabaseName);
    CreateSqlDatabaseUser(masterContext, agendaDatabaseName, userName, userPassword);
    MigrateSqlDatabase(masterContext, agendaDatabaseName);

    using var agendaContext = CreateAgendaContext();
    CleanAgendaDatabase(agendaContext);
  }
}