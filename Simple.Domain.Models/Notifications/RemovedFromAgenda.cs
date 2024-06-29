
namespace Simple.Domain.Models;

public record RemovedFromAgendaNotification: Notification;

partial class ModelsFuncs
{
  public const string RemovedFromAgendaTitle = "Removed from agenda";

  public static AddedToAgendaNotification CreateRemovedFromAgendaNotification (string from, string to, DateTimeOffset date) =>
    new () { From = from, To = to, Title = RemovedFromAgendaTitle, Content  = $"You have been removed from {from}'s agenda", Date = date };
}