global using System;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Configuration;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Simple.Domain.Models;
global using Simple.Shared.Models;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Domain.Queries.QueriesFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using static Simple.Testing.Models.ModelsFuncs;
global using static Simple.Worker.WorkerFuncs;
using MongoDB.Driver;
using Simple.Infrastructure.MongoDb;
using Simple.Infrastructure.SqlServer;

namespace Simple.Worker;

[TestClass]
public partial class WorkerTests
{
  static IHost HostServer = default!;
  static readonly IConfiguration Configuration = BuildConfiguration("settings.json");
  static readonly string SqlConnectionString = CreateSqlConnectionString(GetConfigurationOptions<SqlServerOptions>(Configuration));
  static readonly IMongoDatabase MongoDatabase = GetMongoDatabase(GetConfigurationOptions<MongoOptions>(Configuration));
  static readonly CancellationTokenSource CancellationTokenSource = new(Timeout.Infinite);

  [AssemblyInitialize]
  public static void InitializeTests(TestContext _)
  {
    HostServer = InitializeHostServer([], "settings.json", (_) => { }, CancellationTokenSource.Token);
    RunSynchronously(() => HostServer.StartAsync());
  }

  [AssemblyCleanup]
  public static void CleanupTests()
  {
    RunSynchronously(() => HostServer.StopAsync());
    CancellationTokenSource.Cancel();
  }
}