
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Simple.Infrastructure.SqlServer;
global using static Simple.Api.Endpoints.EndpointsFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerTests;
global using static Simple.Testing.Models.ModelsFuncs;
global using static Simple.Testing.Http.HttpFuncs;
global using static Storing.SqlServer.SqlServerFuncs;
using MongoDB.Driver;

namespace Simple.Api.Endpoints;

[TestClass]
public partial class EndpointsTests
{
  static readonly string SqlConnectionString = CreateSqlConnectionString("agenda-api-tests", "dbuser", "dbuser.P@ssw0rd!", "127.0.0.1");
  static readonly AgendaContextFactory SqlContextFactory = CreateAgendaContextFactory(SqlConnectionString);
  static readonly IMongoDatabase MongoDatabase = GetMongoDatabase("127.0.0.1", 27017, "agenda-api-tests");

  [AssemblyInitialize]
  public static void InitializeTests(TestContext _)
  {
    InitializeSqlDatabase("agenda-api-tests", "sa", "P@ssw0rd!", "dbuser", "dbuser.P@ssw0rd!", "127.0.0.1");
    InitializeMongoDatabase();
  }
}