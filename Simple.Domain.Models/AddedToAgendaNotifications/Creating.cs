
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static AddedToAgendaNotification CreateAddedToAgendaNotification(string from, string to, DateTimeOffset? date = default) =>
    new(from, to, "Added to agenda", $"You have been added to {from}'s agenda", date ?? DateTimeOffset.UtcNow);
}