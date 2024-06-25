
namespace Simple.Api;

partial class ApiFuncs
{
  internal static WebApplication MapEndpoints (WebApplication app, ServerIntegrations serverIntegrations, ILoggerFactory loggerFactory)
  {
    var domainLogger = loggerFactory.CreateLogger(typeof(ServicesFuncs).Namespace!);
    var (mongoIntegration, sqlIntegration) = serverIntegrations;

    MapMongoEndpoints(app, mongoIntegration.MongoDatabase, mongoIntegration.MongoMessageQueue, domainLogger);
    MapSqlEndpoints(app, sqlIntegration.SqlContextFactory, sqlIntegration.SqlMessageQueue, domainLogger);

    return app;
  }
}
