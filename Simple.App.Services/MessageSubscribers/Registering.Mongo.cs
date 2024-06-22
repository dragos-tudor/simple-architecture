
using MongoDB.Driver;

namespace Simple.App.Services;

partial class ServicesFuncs
{
  public static Subscriber<Message>[] RegisterMongoSubscribers (IMongoDatabase agendaDatabase, EmailServerOptions emailServerOptions, Channel<Message> queue, ILogger logger, TimeProvider? timeProvider = default)
  {
    SendNotification<Notification> sendNotification = (notification, cancellationToken) => SendNotificationAsync(notification, emailServerOptions, cancellationToken);
    return [
      CreateSubscriber<Message, ContactCreatedEvent>("notify_added_to_agenda", (message, cancellationToken) =>
        NotifyAddedToAgendaMongoHandler ((Message<ContactCreatedEvent>)message, agendaDatabase, sendNotification, timeProvider ?? TimeProvider.System, logger, cancellationToken)),
    ];
  }
}