
namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static Subscriber<Message>[] RegisterSqlSubscribers (TimeProvider timeProvider, AgendaContextFactory agendaContextFactory, SendNotification<Notification> sendNotification, Channel<Message> queue) =>
  [
    CreateSubscriber<Message, ContactCreatedEvent>("notify_added_to_agenda_sql", (message, cancellationToken) =>
      NotifyAddedToAgendaSqlHandler ((Message<ContactCreatedEvent>)message, timeProvider, agendaContextFactory, sendNotification, cancellationToken)),
  ];
}