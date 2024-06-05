
namespace Simple.Domain.Models;

public record Notification
{
  public string From {get; init; } = string.Empty;
  public string To { get; init; } = string.Empty;
  public string Title { get; init; } = string.Empty;
  public string Content { get; init; } = string.Empty;
  public DateTimeOffset Date { get; init; }
}