global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Simple.Shared.Models.ModelsTests;
global using SqlFuncs = Storing.SqlServer.SqlServerFuncs;

namespace Simple.Infrastructure.SqlServer;

[TestClass]
public partial class SqlServerTests
{
  [AssemblyInitialize]
  public static void InitializeSqlServer(TestContext _)
  {
    SqlServerFuncs.InitializeSqlServer(new SqlServerOptions(
      "sa", "admin.P@ssw0rd",
      "sqluser", "sqluser.P@ssw0rd",
      "mcr.microsoft.com/mssql/server:2019-latest", "simple-sql",
      "agenda", 1433
    ));

    using var agendaContext = CreateAgendaContext();
    CleanAgendaDatabase(agendaContext);
  }
}