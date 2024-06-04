
namespace Simple.Domain.Services;

public record AddedToAgendaNotification (string AddressTo, string Title, string Body);

partial class ServicesFuncs
{
  const string AddedToAgendaTitle = "Added to agenda";

  static string GetAddedToAgendaBody (string agendaOwner) => $"You have been added to {agendaOwner} agenda";

  public static AddedToAgendaNotification CreateAddedToAgendaNotification (string agendaOwner, string addressTo) =>
    new (addressTo, AddedToAgendaTitle, GetAddedToAgendaBody(agendaOwner));
}