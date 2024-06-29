
using Scheduling.JobScheduler;

namespace Simple.Infrastructure.JobScheduler;

public sealed record ResumeMessagesJob: Job
{
  public override string JobName { get; init; } = nameof(ResumeMessagesJob);
  public ResumeMessagesOptions ResumeMessagesOptions { get; init; } = new ();
}