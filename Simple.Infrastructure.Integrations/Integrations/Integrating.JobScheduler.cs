
using Microsoft.Extensions.Configuration;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  public static IDisposable IntegrateJobScheduler (IEnumerable<Job> jobs, IConfiguration configuration, TimeProvider timeProvider, ILoggerFactory loggerFactory)
  {
    var jobSchedulerOptions = GetConfigurationOptions<JobSchedulerOptions>(configuration);
    var jobSchedulerLogger = loggerFactory.CreateLogger(nameof(JobSchedulerFuncs));

    return RunJobs(jobs, jobSchedulerOptions, timeProvider, jobSchedulerLogger);
  }
}