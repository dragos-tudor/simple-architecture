
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Docker.Extensions.DockerFuncs;
global using static Simple.App.AppFuncs;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Shared.Testing.TestingFuncs;

using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;

namespace Simple.App;

[TestClass]
public partial class AppTesting
{
  static AgendaContextFactory AgendaContextFactory = default!;
  static IMongoDatabase AgendaDatabase = default!;
  static IConfiguration Configuration = default!;
  static IHost HostServices = default!;
  static readonly CancellationTokenSource CancellationTokenSource = new (Timeout.Infinite);

  [AssemblyInitialize]
  public static void InitializeTests (TestContext context)
  {
    var configuration = BuildConfiguration("settings.json");
    var host = BuildHost([], configuration, (builder) => {
      builder.Services.AddSingleton<ILoggerFactory>(new NullLoggerFactory());
    });

    var cancellationToken = CancellationTokenSource.Token;
    var loggerFactory = GetRequiredService<ILoggerFactory>(host.Services);
    var serverIntegrations = RunSynchronously(() => IntegrateServersAsync(configuration, RegisterMongoSubscribers, RegisterSqlSubscribers, loggerFactory, cancellationToken));

    var job = GetConfigurationOptions<ResumeMessagesJob>(configuration);
    var jobs = MapResumeMessagesJobAction(job, serverIntegrations, configuration, TimeProvider.System);
    var jobScheduler = IntegrateJobScheduler(jobs, serverIntegrations, configuration, TimeProvider.System, loggerFactory);
    RunSynchronously(() => host.StartAsync(cancellationToken));

    AgendaContextFactory = serverIntegrations.SqlIntegration.SqlContextFactory;
    AgendaDatabase = serverIntegrations.MongoIntegration.MongoDatabase;
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