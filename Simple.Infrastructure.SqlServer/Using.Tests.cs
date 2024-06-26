
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Simple.Shared.Testing.TestingFuncs;

namespace Simple.Infrastructure.SqlServer;

[TestClass]
public partial class SqlServerTests
{
  static readonly SqlServerOptions SqlServerOptions = new SqlServerOptions() {DbName = "agenda-tests", AdminName = "sa", AdminPassword = "admin.P@ssw0rd", UserName = "sqluser", UserPassword = "sqluser.P@ssw0rd"};
  static readonly string SqlConnectionString = CreateSqlConnectionString(SqlServerOptions.DbName, SqlServerOptions.UserName, SqlServerOptions.UserPassword, SqlServerOptions.ContainerName);

  [AssemblyInitialize]
  public static void InitializeSqlServer (TestContext _)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));
    var cancellationToken = cancellationTokenSource.Token;

    RunSynchronously(() =>
      InitializeSqlServerAsync (SqlServerOptions, cancellationToken));
  }
}