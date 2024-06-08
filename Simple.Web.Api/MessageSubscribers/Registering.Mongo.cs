
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static Subscriber<Message, Failure>[] RegisterMongoSubscribers (TimeProvider timeProvider, IMongoDatabase agendaDb, SendNotification<Notification> sendNotification, Channel<Message> queue, ILogger logger) =>
  [
    CreateSubscriber<Message, ContactCreatedEvent, Failure>("notify_added_to_agenda_mongo", (message, cancellationToken) =>
      NotifyAddedToAgendaMongoHandler ((Message<ContactCreatedEvent>)message, timeProvider, agendaDb, sendNotification, logger, cancellationToken)),
  ];
}