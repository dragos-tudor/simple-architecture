
namespace Simple.Api;

partial class ApiFuncs
{
  public static WebApplication InitializeApiServer(
    string[] args,
    string settingsFile,
    Action<WebApplicationBuilder> configBuilder,
    CancellationToken cancellationToken = default)
  {
    var configuration = BuildConfiguration(settingsFile);
    var apiServer = BuildApiServer(args, configuration, configBuilder);

    var logger = GetRequiredService<ILoggerFactory>(apiServer.Services).CreateLogger(typeof(ApiFuncs).FullName!);
    var timeProvider = GetRequiredService<TimeProvider>(apiServer.Services);
    var mailServerOptions = GetConfigurationOptions<MailServerOptions>(configuration);

    var sqlServerOptions = GetConfigurationOptions<SqlServerOptions>(configuration);
    var sqlConnectionString = CreateSqlConnectionString(sqlServerOptions);
    InitializeSqlDatabase(sqlServerOptions);

    var sqlMessageQueue = CreateMessageQueue<Message>(1000);
    ProcessMessageSqlAsync(sqlMessageQueue, sqlConnectionString, 10, mailServerOptions, timeProvider, logger, cancellationToken);
    MapSqlEndpoints(apiServer, sqlConnectionString, sqlMessageQueue);

    var mongoOptions = GetConfigurationOptions<MongoOptions>(configuration);
    var mongoDatabase = GetMongoDatabase(mongoOptions);
    InitializeMongoDatabase();

    var mongoMessageQueue = CreateMessageQueue<Message>(1000);
    ProcessMessageMongoAsync(mongoMessageQueue, mongoDatabase, 10, mailServerOptions, timeProvider, logger, cancellationToken);
    MapMongoEndpoints(apiServer, mongoDatabase, mongoMessageQueue);

    return apiServer;
  }
}