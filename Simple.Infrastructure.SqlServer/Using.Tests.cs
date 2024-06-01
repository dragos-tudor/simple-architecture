
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Simple.Domain.Models.ModelsTests;

namespace Simple.Infrastructure.SqlServer;

[TestClass]
public partial class SqlServerTests
{
  [AssemblyInitialize]
  public static void InitializeSqlServer (TestContext _)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));
    var cancellationToken = cancellationTokenSource.Token;
    var sqlServerOptions = new SqlServerOptions(
      "sa", "admin.P@ssw0rd",
      "sqluser", "sqluser.P@ssw0rd",
      "mcr.microsoft.com/mssql/server:2019-latest", "simple-sql",
      "agenda", 1433, "simple-network"
    );

    RunSynchronously(() => InitializeSqlServerAsync (sqlServerOptions, cancellationToken));

    using var agendaContext = CreateAgendaContext();
    CleanAgendaDatabase(agendaContext);
  }
}