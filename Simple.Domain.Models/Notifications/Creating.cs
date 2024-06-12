
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static Notification CreateNotification (string from, string to, string title, string content, DateTimeOffset date) =>
    new (from, to, title, content, date);

  public static AddedToAgendaNotification CreateAddedToAgendaNotification (string from, string to, DateTimeOffset date) =>
    new (from, to, "Added to agenda", $"You have been added to {from}'s agenda", date);
}