
namespace Simple.Worker;

partial class WorkerFuncs
{
  public static IHost InitializeWorkerServer(
    string[] args,
    string settingsFile,
    Action<HostApplicationBuilder> configBuilder,
    CancellationToken cancellationToken = default)
  {
    var configuration = BuildConfiguration(settingsFile);
    var host = BuildWorkerServer(args, configuration, configBuilder);

    var logger = GetRequiredService<ILoggerFactory>(host.Services).CreateLogger(typeof(WorkerFuncs).FullName!);
    var timeProvider = GetRequiredService<TimeProvider>(host.Services);
    var jobSchedulerOptions = GetConfigurationOptions<JobSchedulerOptions>(configuration);
    var mailServerOptions = GetConfigurationOptions<MailServerOptions>(configuration);

    var sqlServerOptions = GetConfigurationOptions<SqlServerOptions>(configuration);
    var sqlConnectionString = CreateSqlConnectionString(sqlServerOptions);
    InitializeSqlDatabase(sqlServerOptions);

    var mongoOptions = GetConfigurationOptions<MongoOptions>(configuration);
    var mongoDatabase = GetMongoDatabase(mongoOptions);
    InitializeMongoDatabase();

    var sqlMessageJob = GetConfigurationOptions<MessageJob>(configuration, "SqlMessageJob");
    sqlMessageJob = sqlMessageJob with
    {
      JobAction = async () => await ProcessMessageSqlAsync(sqlConnectionString, 10, sqlMessageJob.MessagesJobOptions, mailServerOptions, timeProvider, logger, cancellationToken)
    };

    var mongoMessageJob = GetConfigurationOptions<MessageJob>(configuration, "MongoMessageJob");
    mongoMessageJob = mongoMessageJob with
    {
      JobAction = async () => await ProcessMessageMongoAsync(mongoDatabase, 10, mongoMessageJob.MessagesJobOptions, mailServerOptions, timeProvider, logger, cancellationToken)
    };
    RunJobs([sqlMessageJob, mongoMessageJob], jobSchedulerOptions, timeProvider, logger);

    return host;
  }
}