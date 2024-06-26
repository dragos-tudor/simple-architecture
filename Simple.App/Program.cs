#pragma warning disable CA2000

namespace Simple.App;

partial class AppFuncs
{
  public static async Task Main(string[] args)
  {
    var cancellationTokenSource = new CancellationTokenSource(Timeout.Infinite);
    var cancellationToken = cancellationTokenSource.Token;

    var configuration = BuildConfiguration("settings.json");
    var host = BuildHost(args, configuration, (_) => {});

    var loggerFactory = GetRequiredService<ILoggerFactory>(host.Services);
    var serverIntegrations = await IntegrateServersAsync(configuration, RegisterMongoSubscribers,  RegisterSqlSubscribers, loggerFactory, cancellationToken);

    var job = GetConfigurationOptions<ResumeMessagesJob>(configuration);
    var jobs = MapResumeMessagesJobAction(job, serverIntegrations, configuration, TimeProvider.System);
    var jobScheduler = IntegrateJobScheduler(jobs, serverIntegrations, configuration, TimeProvider.System, loggerFactory);

    var hostLifetime = GetRequiredService<IHostApplicationLifetime>(host.Services);
    hostLifetime.ApplicationStopping.Register(cancellationTokenSource.Cancel);
    hostLifetime.ApplicationStopping.Register(jobScheduler.Dispose);
    await host.RunAsync();
  }
}
