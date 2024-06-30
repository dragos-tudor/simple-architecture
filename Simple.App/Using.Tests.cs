
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Docker.Extensions.DockerFuncs;
global using Simple.Domain.Models;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using static Simple.Shared.Testing.TestingFuncs;

namespace Simple.App;

[TestClass]
public partial class AppTests
{
  static readonly CancellationTokenSource CancellationTokenSource = new (TimeSpan.FromMinutes(10)); // allow pulling images [one-time], starting/restarting containers, open server ports
  static IDisposable JobScheduler = default!;
  static ServerIntegrations ServerIntegrations = default!;

  [AssemblyInitialize]
  public static void InitializeTests (TestContext context)
  {
    var configuration = BuildConfiguration("settings.tests.json");
    var loggerFactory = IntegrateSerilog(configuration); // NullLoggerFactory.Instance;

    var cancellationToken = CancellationTokenSource.Token;
    var serverIntegrations = RunSynchronously(() => IntegrateServersAsync(configuration, RegisterMongoSubscribers, RegisterSqlSubscribers, loggerFactory, cancellationToken));
    var jobScheduler = IntegrateJobScheduler(serverIntegrations, configuration, TimeProvider.System, loggerFactory);

    JobScheduler = jobScheduler;
    ServerIntegrations = serverIntegrations;
  }

  [AssemblyCleanup]
  public static void CleanupTests ()
  {
    CancellationTokenSource.Cancel();
    JobScheduler.Dispose();
  }
}