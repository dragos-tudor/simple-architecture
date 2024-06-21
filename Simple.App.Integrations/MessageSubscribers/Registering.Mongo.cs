
using MongoDB.Driver;

namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  public static Subscriber<Message>[] RegisterMongoSubscribers (TimeProvider timeProvider, IMongoDatabase agendaDatabase, SendNotification<Notification> sendNotification, Channel<Message> queue, ILogger logger) =>
  [
    CreateSubscriber<Message, ContactCreatedEvent>("notify_added_to_agenda", (message, cancellationToken) =>
      NotifyAddedToAgendaMongoHandler ((Message<ContactCreatedEvent>)message, agendaDatabase, sendNotification, timeProvider, logger, cancellationToken)),
  ];
}