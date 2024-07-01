
namespace Simple.Domain.Models;

public record RemovedFromAgendaNotification(string From, string To, string Title, string Content, DateTimeOffset Date): INotification;

partial class ModelsFuncs
{
  public const string RemovedFromAgendaTitle = "Removed from agenda";

  public static AddedToAgendaNotification CreateRemovedFromAgendaNotification (string from, string to, DateTimeOffset? date = default) =>
    new (from, to, RemovedFromAgendaTitle, $"You have been removed from {from}'s agenda", date ?? DateTimeOffset.UtcNow);
}