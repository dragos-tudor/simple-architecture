global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Simple.Shared.Extensions.ExtensionsFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerTests;

namespace Simple.Infrastructure.SqlServer;

[TestClass]
public partial class SqlServerTests
{
  [AssemblyInitialize]
  public static void InitializeSqlServerTest(TestContext _)
  {
    InitializeSqlServer();

    using var agendaContext = CreateAgendaContext();
    agendaContext.Database.ExecuteSqlRaw(@"
      DELETE FROM [PhoneNumbers];
      DELETE FROM [Contacts];
      DELETE FROM [Messages];
    ");
  }
}