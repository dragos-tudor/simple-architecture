
using MongoDB.Driver;

namespace Simple.App.Handlers;

partial class HandlersFuncs
{
  public static async Task<Failure?> NotifyAddedToAgendaMongoHandler (
    Message<ContactCreatedEvent> message,
    TimeProvider timeProvider,
    IMongoDatabase agendaDdb,
    SendNotification<Notification> sendNotification,
    ILogger logger,
    CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(agendaDdb);
    await NotifyAddedToAgendaService (
      message,
      "dragos.tudor@gmail.com",
      timeProvider.GetUtcNow(),
      (messageIdempotency, cancellationToken) => FindMessageDuplication(messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync(cancellationToken) as Task<Message?>,
      (notification, cancellationToken) => sendNotification(notification, cancellationToken),
      (message, cancellationToken) => InsertMessage(messages, message, cancellationToken),
      logger,
      cancellationToken
    );
    return default;
  }
}
