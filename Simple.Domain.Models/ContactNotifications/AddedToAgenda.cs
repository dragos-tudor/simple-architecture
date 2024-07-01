
namespace Simple.Domain.Models;

public record AddedToAgendaNotification(string From, string To, string Title, string Content, DateTimeOffset Date): INotification;

partial class ModelsFuncs
{
  public const string AddedToAgendaTitle = "Added to agenda";

  public static AddedToAgendaNotification CreateAddedToAgendaNotification (string from, string to, DateTimeOffset? date = default) =>
    new (from, to, AddedToAgendaTitle, $"You have been added to {from}'s agenda", date ?? DateTimeOffset.UtcNow);
}