
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Simple.Infrastructure.SqlServer;
global using static Docker.Extensions.DockerFuncs;
global using static Simple.Shared.Testing.TestingFuncs;
global using static Storing.SqlServer.SqlServerFuncs;


using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;
using Simple.Infrastructure.MongoDb;

namespace Simple.Infrastructure.Integrations;

[TestClass]
public partial class IntegrationsTests
{
  static readonly ILogger Logger = new NullLoggerFactory().CreateLogger(nameof(IntegrationsTests));
  static readonly MongoReplicaSetOptions MongoReplicaSetOptions = new () { ContainerNames = ["architecture-mongo1", "architecture-mongo2", "architecture-mongo3"], CollNames = ["contacts", "messages"], DbName = "agenda-tests" };
  static readonly IMongoDatabase MongoDatabase = GetMongoDatabase(MongoReplicaSetOptions);

  static readonly SqlServerOptions SqlServerOptions = new () {DbName = "agenda-tests", AdminName = "sa", AdminPassword = "admin.P@ssw0rd", UserName = "sqluser", UserPassword = "sqluser.P@ssw0rd"};
  static readonly string SqlConnectionString = CreateSqlConnectionString(SqlServerOptions.DbName, SqlServerOptions.UserName, SqlServerOptions.UserPassword, SqlServerOptions.ContainerName);
  static readonly AgendaContextFactory SqlContextFactory = new (CreateSqlContextOptions<AgendaContext>(SqlConnectionString));

  [AssemblyInitialize]
  public static void InitializeTests (TestContext context)
  {
    RunSynchronously(() => InitializeSqlServerAsync(SqlServerOptions));
    RunSynchronously(() => InitializeMongeReplicaSetAsync(MongoReplicaSetOptions));
  }
}