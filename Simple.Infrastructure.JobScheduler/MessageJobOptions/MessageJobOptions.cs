
namespace Simple.Infrastructure.JobScheduler;

public sealed record MessageJobOptions
{
  public TimeSpan MinTime { get; init; } = TimeSpan.FromDays(3);
  public TimeSpan MaxTime { get; init; } = TimeSpan.FromMinutes(10);
  public int BatchSize { get; init; } = 20;
}