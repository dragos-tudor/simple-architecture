#pragma warning disable CS4014

namespace Simple.Api;

partial class ApiFuncs
{
  public static async Task<WebApplication> StartAppAsync(
    string[] args,
    string settingsFile,
    Action<WebApplicationBuilder> configBuilder,
    CancellationToken cancellationToken = default)
  {
    var configuration = BuildConfiguration(settingsFile);
    var app = BuildApplication(args, configuration, configBuilder);
    var logger = GetRequiredService<ILogger>(app.Services);

    var sqlServerOptions = GetConfigurationOptions<SqlServerOptions>(configuration);
    var sqlConnectionString = CreateSqlConnectionString(sqlServerOptions);
    var dbContextFactory = CreateAgendaContextFactory(sqlConnectionString);
    InitializeSqlDatabase(sqlServerOptions);

    var sqlMessageQueue = CreateMessageQueue<Message>(1000);
    MapSqlEndpoints(app, dbContextFactory, sqlMessageQueue);
    ProcessMessageSqlAsync(sqlMessageQueue, dbContextFactory, 10, logger, cancellationToken);

    var mongoOptions = GetConfigurationOptions<MongoOptions>(configuration);
    var mongoDatabase = GetMongoDatabase(mongoOptions);
    InitializeMongoDatabase();

    var mongoMessageQueue = CreateMessageQueue<Message>(1000);
    MapMongoEndpoints(app, mongoDatabase, mongoMessageQueue);
    ProcessMessageMongoAsync(mongoMessageQueue, mongoDatabase, 10, logger, cancellationToken);

    await app.StartAsync(cancellationToken);
    return app;
  }

  public static async Task Main(string[] args)
  {
    var app = await StartAppAsync(args, "settings.json", (_) => { }, CancellationToken.None);
    await app.RunAsync();
  }
}