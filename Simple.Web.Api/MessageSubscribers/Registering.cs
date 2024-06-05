
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static Subscriber<Message>[] RegisterSqlSubscribers (TimeProvider timeProvider, AgendaContextFactory agendaContextFactory, SendNotification<Notification> sendNotification, Channel<Message> queue) =>
  [
    CreateSubscriber<Message, ContactCreatedEvent>("notify_added_to_agenda_sql", (message, cancellationToken) =>
      NotifyAddedToAgendaSqlHandler ((Message<ContactCreatedEvent>)message, timeProvider, agendaContextFactory, sendNotification, queue, cancellationToken)),
  ];

  public static Subscriber<Message>[] RegisterMongoSubscribers (TimeProvider timeProvider, IMongoDatabase agendaDb, SendNotification<Notification> sendNotification, Channel<Message> queue) =>
  [
    CreateSubscriber<Message, ContactCreatedEvent>("notify_added_to_agenda_mongo", (message, cancellationToken) =>
      NotifyAddedToAgendaMongoHandler ((Message<ContactCreatedEvent>)message, timeProvider, agendaDb, sendNotification, queue, cancellationToken)),
  ];
}