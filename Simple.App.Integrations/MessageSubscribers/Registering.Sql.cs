
namespace Simple.App.Integrations;

partial class IntegrationFuncs
{
  public static Subscriber<Message>[] RegisterSqlSubscribers (TimeProvider timeProvider, AgendaContextFactory agendaContextFactory, SendNotification<Notification> sendNotification, Channel<Message> queue, ILogger logger) =>
  [
    CreateSubscriber<Message, ContactCreatedEvent>("notify_added_to_agenda_sql", (message, cancellationToken) =>
      NotifyAddedToAgendaSqlHandler ((Message<ContactCreatedEvent>)message, timeProvider, agendaContextFactory, sendNotification, logger, cancellationToken)),
  ];
}