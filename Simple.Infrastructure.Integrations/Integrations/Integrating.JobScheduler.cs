
using Microsoft.Extensions.Configuration;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  public static IDisposable IntegrateJobScheduler (IEnumerable<Job> jobs, ServerIntegrations serverIntegrations, IConfiguration configuration, TimeProvider timeProvider, ILoggerFactory loggerFactory)
  {
    var mongoIntegration = serverIntegrations.MongoIntegration;
    var sqlIntegration = serverIntegrations.SqlIntegration;

    var jobSchedulerOptions = GetConfigurationOptions<JobSchedulerOptions>(configuration);
    var messageHandlerOptions = GetConfigurationOptions<MessageHandlerOptions>(configuration);
    var jobSchedulerLogger = loggerFactory.CreateLogger(nameof(JobSchedulerFuncs));

    return RunJobs(jobs, jobSchedulerOptions, timeProvider, jobSchedulerLogger);
  }
}