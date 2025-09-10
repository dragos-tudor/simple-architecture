
using Scheduling.JobScheduler;

namespace Simple.Infrastructure.JobScheduler;

public sealed record MessageJob : Job
{
  public override string JobName { get; init; } = nameof(MessageJob);
  public MessageJobOptions MessagesJobOptions { get; init; } = new();
}