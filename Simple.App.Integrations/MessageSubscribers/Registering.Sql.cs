
namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  public static Subscriber<Message>[] RegisterSqlSubscribers (TimeProvider timeProvider, AgendaContextFactory agendaContextFactory, SendNotification<Notification> sendNotification, Channel<Message> queue, ILogger logger) =>
  [
    CreateSubscriber<Message, ContactCreatedEvent>("notify_added_to_agenda", (message, cancellationToken) =>
      NotifyAddedToAgendaSqlHandler ((Message<ContactCreatedEvent>)message, agendaContextFactory, sendNotification, timeProvider, logger, cancellationToken)),
  ];
}