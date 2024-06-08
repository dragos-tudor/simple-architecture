
namespace Simple.Domain.Models;

public record Failure (string Message, string? Source = default)
{
  public override string ToString() => $"{GetType().Name}: {Message} {Source}";
}

public record ValidationFailure (string Message, string? Source = default): Failure(Message, Source);