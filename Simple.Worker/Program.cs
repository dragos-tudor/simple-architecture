
namespace Simple.Worker;

partial class WorkerFuncs
{
  public static IHost InitializeHost(
    string[] args,
    string settingsFile,
    Action<HostApplicationBuilder> configBuilder,
    CancellationToken cancellationToken = default)
  {
    var configuration = BuildConfiguration(settingsFile);
    var host = BuildHost(args, configuration, configBuilder);

    var logger = GetRequiredService<ILoggerFactory>(host.Services).CreateLogger(nameof(WorkerFuncs));
    var timeProvider = GetRequiredService<TimeProvider>(host.Services);
    var jobSchedulerOptions = GetConfigurationOptions<JobSchedulerOptions>(configuration);
    var mailServerOptions = GetConfigurationOptions<MailServerOptions>(configuration);
    var messageJob = GetConfigurationOptions<MessageJob>(configuration);

    var sqlServerOptions = GetConfigurationOptions<SqlServerOptions>(configuration);
    var sqlConnectionString = CreateSqlConnectionString(sqlServerOptions);
    InitializeSqlDatabase(sqlServerOptions);

    var mongoOptions = GetConfigurationOptions<MongoOptions>(configuration);
    var mongoDatabase = GetMongoDatabase(mongoOptions);
    InitializeMongoDatabase();

    var sqlMessageJob = messageJob with
    {
      JobName = "SqlMessageJob",
      JobAction = () => ProcessMessageSqlAsync(sqlConnectionString, 10, messageJob.MessagesJobOptions, mailServerOptions, timeProvider, logger, cancellationToken)
    };
    var mongoMessageJob = messageJob with
    {
      JobName = "MongoMessageJob",
      JobAction = () => ProcessMessageMongoAsync(mongoDatabase, 10, messageJob.MessagesJobOptions, mailServerOptions, timeProvider, logger, cancellationToken)
    };
    RunJobs([sqlMessageJob, mongoMessageJob], jobSchedulerOptions, timeProvider, logger);

    return host;
  }

  public static async Task Main(string[] args)
  {
    var host = InitializeHost(args, "settings.json", (_) => { }, CancellationToken.None);
    await host.RunAsync();
  }
}