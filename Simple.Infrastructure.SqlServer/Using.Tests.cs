
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Simple.Domain.Queries.QueriesFuncs;
global using static Simple.Testing.Models.ModelsFuncs;

namespace Simple.Infrastructure.SqlServer;

[TestClass]
public partial class SqlServerTests
{
  static readonly string SqlConnectionString = CreateSqlConnectionString("agenda-tests", "dbuser", "dbuser.P@ssw0rd!", "127.0.0.1");

  [AssemblyInitialize]
  public static void InitializeTests(TestContext _) =>
    InitializeSqlDatabase("agenda-tests", "sa", "P@ssw0rd!", "dbuser", "dbuser.P@ssw0rd!", "127.0.0.1");
}