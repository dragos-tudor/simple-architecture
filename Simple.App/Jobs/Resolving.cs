
using Simple.Infrastructure.Mediator;

namespace Simple.App;

partial class AppFuncs
{
  internal static ResumeMessagesJob[] ResolveResumeMessagesJobs (ServerIntegrations serverIntegrations, IConfiguration configuration, TimeProvider timeProvider, CancellationToken cancellationToken = default)
  {
    var (mongoDatabase, mongoMessageQueue) = serverIntegrations.MongoIntegration;
    var (sqlContextFactory, sqlMessageQueue) = serverIntegrations.SqlIntegration;
    var resumeMessagesJob = GetConfigurationOptions<ResumeMessagesJob>(configuration);
    var messageHandlerOptions = GetConfigurationOptions<MessageHandlerOptions>(configuration);

    return [
      resumeMessagesJob with { JobAction = () => ResumeMessagesMongoAsync(mongoDatabase, mongoMessageQueue, messageHandlerOptions, timeProvider, cancellationToken) },
      resumeMessagesJob with { JobAction = () => ResumeMessagesSqlAsync(sqlContextFactory, sqlMessageQueue, messageHandlerOptions, timeProvider, cancellationToken) }
    ];
  }
}