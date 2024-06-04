
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  const string AddedToAgendaTitle = "Added to agenda";

  public static AddedToAgendaNotification CreateAddedToAgendaNotification (string agendaOwner, string addressTo) =>
    new (addressTo, AddedToAgendaTitle, GetAddedToAgendaBody(agendaOwner));
}