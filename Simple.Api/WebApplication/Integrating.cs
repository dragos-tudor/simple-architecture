
namespace Simple.Api;

partial class ApiFuncs
{
  static WebApplication IntegrateApi (WebApplication app, ServerIntegrations serverIntegrations, ILoggerFactory loggerFactory)
  {
    var domainLogger = loggerFactory.CreateLogger(typeof(ServicesFuncs).Namespace!);
    var (mongoIntegration, sqlIntegration) = serverIntegrations;

    MapMongoEndpoints(app, mongoIntegration.MongoDatabase, mongoIntegration.MongoMessageQueue, domainLogger);
    MapSqlEndpoints(app, sqlIntegration.SqlContextFactory, sqlIntegration.SqlMessageQueue, domainLogger);

    return app;
  }

  internal static async Task<ServerIntegrations> IntegrateServersAndApiAsync (WebApplication app, IConfiguration configuration, CancellationToken cancellationToken = default)
  {
    var loggerFactory = GetRequiredService<ILoggerFactory>(app.Services);

    var serverIntegrations = await IntegrateServersAsync(configuration, RegisterMongoSubscribers, RegisterSqlSubscribers, loggerFactory, cancellationToken);
    IntegrateApi(app, serverIntegrations, loggerFactory);

    return serverIntegrations;
  }
}
