
namespace Simple.Api;

partial class ApiFuncs
{
  public static WebApplication InitializeApp(
    string[] args,
    string settingsFile,
    Action<WebApplicationBuilder> configBuilder,
    CancellationToken cancellationToken = default)
  {
    var configuration = BuildConfiguration(settingsFile);
    var app = BuildApplication(args, configuration, configBuilder);
    var logger = GetRequiredService<ILoggerFactory>(app.Services).CreateLogger(nameof(ApiFuncs));
    var timeProvider = GetRequiredService<TimeProvider>(app.Services);
    var mailServerOptions = GetConfigurationOptions<MailServerOptions>(configuration);

    var sqlServerOptions = GetConfigurationOptions<SqlServerOptions>(configuration);
    var sqlConnectionString = CreateSqlConnectionString(sqlServerOptions);
    var dbContextFactory = CreateAgendaContextFactory(sqlConnectionString);
    InitializeSqlDatabase(sqlServerOptions);

    var sqlMessageQueue = CreateMessageQueue<Message>(1000);
    MapSqlEndpoints(app, dbContextFactory, sqlMessageQueue);
    ProcessMessageSqlAsync(sqlMessageQueue, dbContextFactory, 10, mailServerOptions, timeProvider, logger, cancellationToken);

    var mongoOptions = GetConfigurationOptions<MongoOptions>(configuration);
    var mongoDatabase = GetMongoDatabase(mongoOptions);
    InitializeMongoDatabase();

    var mongoMessageQueue = CreateMessageQueue<Message>(1000);
    MapMongoEndpoints(app, mongoDatabase, mongoMessageQueue);
    ProcessMessageMongoAsync(mongoMessageQueue, mongoDatabase, 10, mailServerOptions, timeProvider, logger, cancellationToken);

    return app;
  }

  public static async Task Main(string[] args)
  {
    var app = InitializeApp(args, "settings.json", (_) => { }, CancellationToken.None);
    await app.RunAsync();
  }
}