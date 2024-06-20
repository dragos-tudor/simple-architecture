
namespace Simple.Infrastructure.Mediator;

public sealed record MessageHandlerOptions
{
  public TimeSpan HandleTimeout { get; init; } = TimeSpan.FromSeconds(15);
  public byte MaxFailures { get; init; } = 10;
}