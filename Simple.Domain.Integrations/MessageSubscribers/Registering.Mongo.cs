
using MongoDB.Driver;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static Subscriber<Message>[] RegisterMongoSubscribers (IMongoDatabase mongoDatabase, EmailServerOptions emailServerOptions, Channel<Message> queue, ILogger logger, TimeProvider? timeProvider = default)
  {
    SendNotification<INotification> sendNotification = (notification, cancellationToken) => SendNotificationAsync(notification, emailServerOptions, cancellationToken);
    return [
      CreateSubscriber<Message, ContactCreatedEvent>("notify_added_to_agenda", (message, cancellationToken) =>
        NotifyAddedToAgendaMongoHandler ((Message<ContactCreatedEvent>)message, mongoDatabase, sendNotification, timeProvider ?? TimeProvider.System, logger, cancellationToken)),
    ];
  }
}