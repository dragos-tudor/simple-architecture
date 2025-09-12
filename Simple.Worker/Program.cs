
namespace Simple.Worker;

partial class WorkerFuncs
{
  public static async Task<IHost> StartHostAsync(
    string[] args,
    string settingsFile,
    Action<HostApplicationBuilder> configBuilder,
    CancellationToken cancellationToken = default)
  {
    var configuration = BuildConfiguration(settingsFile);
    var host = BuildHost(args, configuration, configBuilder);

    var logger = GetRequiredService<ILogger>(host.Services);
    var timeProvider = GetRequiredService<TimeProvider>(host.Services);
    var jobSchedulerOptions = GetConfigurationOptions<JobSchedulerOptions>(configuration);
    var mailServerOptions = GetConfigurationOptions<MailServerOptions>(configuration);
    var messageJob = GetConfigurationOptions<MessageJob>(configuration);

    var sqlServerOptions = GetConfigurationOptions<SqlServerOptions>(configuration);
    var dbContextFactory = CreateAgendaContextFactory(sqlServerOptions);
    InitializeSqlDatabase(sqlServerOptions);

    var sqlMessageJob = messageJob with
    {
      JobAction = () =>
        ProcessMessageSqlAsync(dbContextFactory, 10, messageJob.MessagesJobOptions, mailServerOptions, timeProvider, logger, cancellationToken)
    };
    RunJobs([sqlMessageJob], jobSchedulerOptions, timeProvider, logger);

    var mongoOptions = GetConfigurationOptions<MongoOptions>(configuration);
    var mongoDatabase = GetMongoDatabase(mongoOptions);
    InitializeMongoDatabase();

    var mongoMessageJob = messageJob with
    {
      JobAction = () =>
        ProcessMessageMongoAsync(mongoDatabase, 10, messageJob.MessagesJobOptions, mailServerOptions, timeProvider, logger, cancellationToken)
    };
    RunJobs([mongoMessageJob], jobSchedulerOptions, timeProvider, logger);

    await host.StartAsync(cancellationToken);
    return host;
  }

  public static async Task Main(string[] args)
  {
    var host = await StartHostAsync(args, "settings.json", (_) => { }, CancellationToken.None);
    await host.RunAsync();
  }
}