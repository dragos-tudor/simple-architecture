
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static async Task<string?> NotifyAddedToAgendaMongoHandler (
    Message<ContactCreatedEvent> message,
    TimeProvider timeProvider,
    IMongoDatabase agendaDdb,
    SendNotification<Notification> sendNotification,
    Channel<Message> messageQueue,
    CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(agendaDdb);
    await NotifyAddedToAgendaApi (
      message,
      "dragos.tudor@gmail.com",
      timeProvider.GetUtcNow(),
      (message, cancellationToken) => FindMessageByParent(messages.AsQueryable(), message.MessageId).FirstOrDefaultAsync(cancellationToken) as Task<Message?>,
      (notification, cancellationToken) => sendNotification(notification, cancellationToken),
      (message, cancellationToken) => InsertMessage(messages, message, cancellationToken),
      cancellationToken
    );
    return default;
  }
}
