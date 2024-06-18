
using MongoDB.Driver;

namespace Simple.App.Integrations;

partial class IntegrationFuncs
{
  public static Subscriber<Message>[] RegisterMongoSubscribers (TimeProvider timeProvider, IMongoDatabase agendaDb, SendNotification<Notification> sendNotification, Channel<Message> queue, ILogger logger) =>
  [
    CreateSubscriber<Message, ContactCreatedEvent>("notify_added_to_agenda_mongo", (message, cancellationToken) =>
      NotifyAddedToAgendaMongoHandler ((Message<ContactCreatedEvent>)message, timeProvider, agendaDb, sendNotification, logger, cancellationToken)),
  ];
}