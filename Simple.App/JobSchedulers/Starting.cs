
using System.Linq;
using Simple.Infrastructure.Mediator;

namespace Simple.App;

partial class AppFuncs
{
  public static IDisposable StartJobScheduler (IEnumerable<Job> jobs, ServerIntegrations serverIntegrations, IConfiguration configuration, TimeProvider timeProvider, ILoggerFactory loggerFactory)
  {
    var mongoIntegration = serverIntegrations.MongoIntegration;
    var sqlIntegration = serverIntegrations.SqlIntegration;

    var jobSchedulerOptions = GetConfigurationOptions<JobSchedulerOptions>(configuration);
    var messageHandlerOptions = GetConfigurationOptions<MessageHandlerOptions>(configuration);
    var jobSchedulerLogger = loggerFactory.CreateLogger(nameof(JobSchedulerFuncs));

    var mongoJobs = MapJobActionsMongo(jobs, mongoIntegration.MongoDatabase, mongoIntegration.MongoMessageQueue, messageHandlerOptions, timeProvider);
    var sqlJobs = MapJobActionsSql(jobs, sqlIntegration.SqlContextFactory, sqlIntegration.SqlMessageQueue, messageHandlerOptions, timeProvider);

    return RunJobs(mongoJobs.Concat(sqlJobs), jobSchedulerOptions, timeProvider, jobSchedulerLogger);
  }
}