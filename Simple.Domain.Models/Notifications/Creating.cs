
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public const string AddedToAgendaTitle = "Added to agenda";

  public static Notification CreateNotification (string from, string to, string title, string content, DateTimeOffset date) =>
    new () { From = from, To = to, Title = title, Content  = content, Date = date };

  public static AddedToAgendaNotification CreateAddedToAgendaNotification (string from, string to, DateTimeOffset date) =>
    new () { From = from, To = to, Title = AddedToAgendaTitle, Content  = $"You have been added to {from}'s agenda", Date = date };
}