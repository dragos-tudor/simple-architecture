
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static async Task<Failure?> NotifyAddedToAgendaMongoHandler (
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
      (message, cancellationToken) => FindMessageByParent(messages.AsQueryable(), message.MessageId).FirstOrDefaultAsync(cancellationToken) as Task<Message?>,
      (notification, cancellationToken) => sendNotification(notification, cancellationToken),
      (message, cancellationToken) => InsertMessage(messages, message, cancellationToken),
      logger,
      cancellationToken
    );
    return default;
  }
}