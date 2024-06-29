
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Microsoft.Extensions.Time.Testing;
global using static Docker.Extensions.DockerFuncs;
global using static Simple.App.AppFuncs;
global using Simple.Domain.Models;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using static Simple.Shared.Testing.TestingFuncs;

using Microsoft.Extensions.Logging.Abstractions;

namespace Simple.App;

[TestClass]
public partial class AppTests
{
  static ServerIntegrations ServerIntegrations = default!;
  static readonly CancellationTokenSource CancellationTokenSource = new (TimeSpan.FromMinutes(10));

  [AssemblyInitialize]
  public static void InitializeTests (TestContext context)
  {
    var configuration = BuildConfiguration("settings.tests.json");
    var loggerFactory = NullLoggerFactory.Instance;

    var cancellationToken = CancellationTokenSource.Token;
    var serverIntegrations = RunSynchronously(() => IntegrateServersAsync(configuration, RegisterMongoSubscribers, RegisterSqlSubscribers, loggerFactory, cancellationToken));
    ServerIntegrations = serverIntegrations;
  }

  [AssemblyCleanup]
  public static void CleanupTests ()
  {
    CancellationTokenSource.Cancel();
  }
}