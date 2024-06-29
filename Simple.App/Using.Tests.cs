
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Microsoft.Extensions.Time.Testing;
global using static Docker.Extensions.DockerFuncs;
global using static Simple.App.AppFuncs;
global using Simple.Domain.Models;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Infrastructure.Queue.QueueFuncs;
global using static Simple.Shared.Testing.TestingFuncs;

using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;

namespace Simple.App;

[TestClass]
public partial class AppTests
{
  static AgendaContextFactory SqlContextFactory = default!;
  static IMongoDatabase MongoDatabase = default!;
  static IConfiguration Configuration = default!;
  static IHost HostServices = default!;
  static readonly CancellationTokenSource CancellationTokenSource = new (Timeout.Infinite);

  [AssemblyInitialize]
  public static void InitializeTests (TestContext context)
  {
    var configuration = BuildConfiguration("settings.tests.json");
    var host = BuildHost([], configuration, (builder) => {
      builder.Services.AddSingleton<ILoggerFactory>(new NullLoggerFactory());
    });

    var cancellationToken = CancellationTokenSource.Token;
    var loggerFactory = GetRequiredService<ILoggerFactory>(host.Services);
    var serverIntegrations = RunSynchronously(() => IntegrateServersAsync(configuration, RegisterMongoSubscribers, RegisterSqlSubscribers, loggerFactory, cancellationToken));
    RunSynchronously(() => host.StartAsync(cancellationToken));

    SqlContextFactory = serverIntegrations.SqlIntegration.SqlContextFactory;
    MongoDatabase = serverIntegrations.MongoIntegration.MongoDatabase;
    Configuration = configuration;
    HostServices = host;
  }

  [AssemblyCleanup]
  public static void CleanupTests ()
  {
    RunSynchronously(() => HostServices.StopAsync());
    CancellationTokenSource.Cancel();
  }
}