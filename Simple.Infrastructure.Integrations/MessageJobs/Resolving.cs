
using Microsoft.Extensions.Configuration;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  public static IEnumerable<Job> ResolveResumeMessagesJobs (ServerIntegrations serverIntegrations, IConfiguration configuration, TimeProvider timeProvider, ILoggerFactory loggerFactory, CancellationToken cancellationToken = default) => [
    ResolveResumeMessagesMongoJob(serverIntegrations.MongoIntegration, configuration, timeProvider, loggerFactory, cancellationToken),
    ResolveResumeMessagesSqlJob(serverIntegrations.SqlIntegration, configuration, timeProvider, loggerFactory, cancellationToken)
  ];

  public static Job ResolveResumeMessagesMongoJob (MongoIntegration mongoIntegration, IConfiguration configuration, TimeProvider timeProvider, ILoggerFactory loggerFactory, CancellationToken cancellationToken = default)
  {
    var (mongoDatabase, mongoMessageQueue) = mongoIntegration;
    var resumeMessagesJob = GetConfigurationOptions<ResumeMessagesJob>(configuration);
    var jobSchedulerLogger = loggerFactory.CreateLogger(nameof(JobScheduler.JobSchedulerFuncs));

    return resumeMessagesJob with { JobAction = () => ResumeMessagesMongoAsync(mongoDatabase, mongoMessageQueue, timeProvider, resumeMessagesJob.ResumeMessagesOptions, jobSchedulerLogger, cancellationToken) };
  }

  public static Job ResolveResumeMessagesSqlJob (SqlIntegration sqlIntegration, IConfiguration configuration, TimeProvider timeProvider, ILoggerFactory loggerFactory, CancellationToken cancellationToken = default)
  {
    var (sqlContextFactory, sqlMessageQueue) = sqlIntegration;
    var resumeMessagesJob = GetConfigurationOptions<ResumeMessagesJob>(configuration);
    var jobSchedulerLogger = loggerFactory.CreateLogger(nameof(JobScheduler.JobSchedulerFuncs));

    return resumeMessagesJob with { JobAction = () => ResumeMessagesSqlAsync(sqlContextFactory, sqlMessageQueue, timeProvider, resumeMessagesJob.ResumeMessagesOptions, jobSchedulerLogger, cancellationToken) };
  }
}