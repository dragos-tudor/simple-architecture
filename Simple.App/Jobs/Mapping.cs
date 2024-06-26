
using Simple.Infrastructure.Mediator;

namespace Simple.App;

partial class AppFuncs
{
  internal static Job[] MapResumeMessageJobAction (ResumeMessagesJob job, ServerIntegrations serverIntegrations, IConfiguration configuration, TimeProvider timeProvider)
  {
    var (mongoIntegration, sqlIntegration) = serverIntegrations;
    var messageHandlerOptions = GetConfigurationOptions<MessageHandlerOptions>(configuration);

    return [
      MapResumeMessagesJobActionMongo(job, mongoIntegration.MongoDatabase, mongoIntegration.MongoMessageQueue, messageHandlerOptions, timeProvider),
      MapResumeMessagesJobActionSql(job, sqlIntegration.SqlContextFactory, sqlIntegration.SqlMessageQueue, messageHandlerOptions, timeProvider)
    ];
  }
}