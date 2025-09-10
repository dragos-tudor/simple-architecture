
namespace Simple.Domain.Models;

public interface INotification
{
  public string From { get; init; }
  public string To { get; init; }
  public string Title { get; init; }
  public string Content { get; init; }
  public DateTimeOffset Date { get; init; }
}