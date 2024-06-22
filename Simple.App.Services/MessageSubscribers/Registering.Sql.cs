
namespace Simple.App.Services;

partial class ServicesFuncs
{
  public static Subscriber<Message>[] RegisterSqlSubscribers (AgendaContextFactory agendaContextFactory, EmailServerOptions emailServerOptions, Channel<Message> queue, ILogger logger, TimeProvider? timeProvider = default)
  {
    SendNotification<Notification> sendNotification = (notification, cancellationToken) => SendNotificationAsync(notification, emailServerOptions, cancellationToken);
    return [
      CreateSubscriber<Message, ContactCreatedEvent>("notify_added_to_agenda", (message, cancellationToken) =>
        NotifyAddedToAgendaSqlHandler ((Message<ContactCreatedEvent>)message, agendaContextFactory, sendNotification, timeProvider ?? TimeProvider.System, logger, cancellationToken)),
    ];
  }
}