
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Simple.Domain.Models;
global using static Docker.Extensions.DockerFuncs;
global using static Simple.App.AppFuncs;

using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;

namespace Simple.App;

[TestClass]
public partial class AppTesting
{
  static readonly CancellationTokenSource CancellationTokenSource = new (Timeout.Infinite);
  static AgendaContextFactory AgendaContextFactory = default!;
  static IMongoDatabase AgendaDatabase = default!;
  static IDisposable JobScheduler = default!;
  static Func<string, string, Predicate<Notification>, Task<IEnumerable<Notification>>> ReceiveNotifications = default!;

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

    AgendaContextFactory = serverIntegrations.SqlIntegration.SqlContextFactory;
    AgendaDatabase = serverIntegrations.MongoIntegration.MongoDatabase;
    JobScheduler = jobScheduler;
    ReceiveNotifications = (userName, password, filterNotification) => ReceiveNotificationsAsync(userName, password, GetEmailServerOptions(configuration), filterNotification);
  }

  [AssemblyCleanup]
  public static void CleanupTests ()
  {
    CancellationTokenSource.Cancel();
    JobScheduler.Dispose();
  }
}