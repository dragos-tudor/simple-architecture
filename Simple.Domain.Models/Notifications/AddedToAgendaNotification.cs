
namespace Simple.Domain.Models;

public record AddedToAgendaNotification: Notification;

partial class ModelsFuncs
{
  public static AddedToAgendaNotification CreateAddedToAgendaNotification (string from, string to, DateTimeOffset date) =>
    new () { From = from, To = to, Title = "Added to agenda", Content = $"You have been added to {from}'s agenda", Date = date};
}