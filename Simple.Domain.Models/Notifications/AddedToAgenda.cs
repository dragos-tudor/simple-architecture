
namespace Simple.Domain.Models;

public record AddedToAgendaNotification: Notification;

partial class ModelsFuncs
{
  public const string AddedToAgendaTitle = "Added to agenda";

  public static AddedToAgendaNotification CreateAddedToAgendaNotification (string from, string to, DateTimeOffset date) =>
    new () { From = from, To = to, Title = AddedToAgendaTitle, Content  = $"You have been added to {from}'s agenda", Date = date };
}