
namespace Simple.Api;

partial class ApiFuncs
{
  internal static async Task<WebApplication> StartAppAsync(
    string[] args,
    string settingsFile,
    Action<WebApplicationBuilder> configBuilder,
    CancellationToken cancellationToken = default)
  {
    var configuration = BuildConfiguration(settingsFile);
    var app = BuildApplication(args, configuration, configBuilder);

    var sqlServerOptions = GetConfigurationOptions<SqlServerOptions>(configuration);
    var sqlConnectionString = CreateSqlConnectionString(sqlServerOptions.DbName, sqlServerOptions.UserName, sqlServerOptions.UserPassword, sqlServerOptions.ServerName);
    var dbContextFactory = CreateAgendaContextFactory(sqlConnectionString);
    InitializeSqlDatabase(sqlServerOptions.DbName, sqlServerOptions.AdminName, sqlServerOptions.AdminPassword, sqlServerOptions.UserName, sqlServerOptions.UserPassword, sqlServerOptions.ServerName);
    MapSqlEndpoints(app, dbContextFactory, CreateMessageQueue<Message>(1000));

    var mongoOptions = GetConfigurationOptions<MongoOptions>(configuration);
    var mongoDatabase = GetMongoDatabase(mongoOptions.ServerName, mongoOptions.ServerPort, mongoOptions.DbName);
    InitializeMongoDatabase();
    MapMongoEndpoints(app, mongoDatabase, CreateMessageQueue<Message>(1000));

    await app.StartAsync(cancellationToken);
    return app;
  }

  public static async Task MainAsync(string[] args)
  {
    var app = await StartAppAsync(args, "settings.json", (_) => { }, CancellationToken.None);
    await app.RunAsync();
  }
}