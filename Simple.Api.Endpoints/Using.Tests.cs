
global using Microsoft.VisualStudio.TestTools.UnitTesting;
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
  static readonly string SqlConnectionString = CreateSqlConnectionString("agenda-api-endpoints", "dbuser", "dbuser.P@ssw0rd!", "127.0.0.1");
  static readonly AgendaContextFactory SqlContextFactory = CreateAgendaContextFactory(SqlConnectionString);
  static readonly IMongoDatabase MongoDatabase = GetMongoDatabase("127.0.0.1", 27017, "agenda-api-tests");

  [AssemblyInitialize]
  public static void InitializeTests(TestContext _)
  {
    InitializeSqlDatabase("agenda-api-endpoints", "sa", "P@ssw0rd!", "dbuser", "dbuser.P@ssw0rd!", "127.0.0.1");
    InitializeMongoDatabase();
  }
}