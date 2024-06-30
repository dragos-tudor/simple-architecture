
using Microsoft.Extensions.Configuration;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  public static IDisposable IntegrateJobScheduler (ServerIntegrations serverIntegrations, IConfiguration configuration, TimeProvider timeProvider, ILoggerFactory loggerFactory, CancellationToken cancellationToken = default)
  {
    var jobSchedulerOptions = GetConfigurationOptions<JobSchedulerOptions>(configuration);
    var jobSchedulerLogger = loggerFactory.CreateLogger(nameof(JobScheduler.JobSchedulerFuncs));
    var jobs = ResolveResumeMessagesJobs(serverIntegrations, configuration, TimeProvider.System, loggerFactory, cancellationToken);

    return RunJobs(jobs, jobSchedulerOptions, timeProvider, jobSchedulerLogger);
  }
}