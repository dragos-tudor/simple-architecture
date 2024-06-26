
using Scheduling.JobScheduler;

namespace Simple.Domain.Integrations;

public sealed record ResumeMessagesJob: Job
{
  public override string JobName { get; init; } = "ResumeMessagesJob";
}