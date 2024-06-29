namespace Simple.Domain.Models;

public record Notification {
  public string From { get; init; } = string.Empty;
  public string To { get; init; } = string.Empty;
  public string Title { get; init; } = string.Empty;
  public string Content { get; init; } = string.Empty;
  public DateTimeOffset Date { get; init; } = DateTimeOffset.UtcNow;
};

partial class ModelsFuncs
{
  public static Notification CreateNotification (string from, string to, string title, string content, DateTimeOffset date) =>
    new () { From = from, To = to, Title = title, Content  = content, Date = date };
}