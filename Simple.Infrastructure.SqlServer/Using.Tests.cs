
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Simple.Shared.Testing.TestingFuncs;

namespace Simple.Infrastructure.SqlServer;

[TestClass]
public partial class SqlServerTests
{
  static readonly string AgendaConnString = CreateSqlConnectionString("agenda", "sqluser", "sqluser.P@ssw0rd", "simple-sql");

  [AssemblyInitialize]
  public static void InitializeSqlServer (TestContext _)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));
    var cancellationToken = cancellationTokenSource.Token;
    var sqlServerOptions = new SqlServerOptions(
      "sa", "admin.P@ssw0rd",
      "sqluser", "sqluser.P@ssw0rd",
      "mcr.microsoft.com/mssql/server:2019-latest", "simple-sql",
      "agenda", "simple-network",
      1433
    );

    RunSynchronously(() =>
      InitializeSqlServerAsync (sqlServerOptions, cancellationToken));
  }
}