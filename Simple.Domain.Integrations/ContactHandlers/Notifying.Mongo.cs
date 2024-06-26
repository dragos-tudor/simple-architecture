
using MongoDB.Driver;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<Failure?> NotifyAddedToAgendaMongoHandler (
    Message<ContactCreatedEvent> message,
    IMongoDatabase agendaDatabase,
    SendNotification<Notification> sendNotification,
    TimeProvider timeProvider,
    ILogger logger,
    CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(agendaDatabase);
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
