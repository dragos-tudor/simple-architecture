
using MongoDB.Driver;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<string?> NotifyAddedToAgendaMongoAsync(
    Message<ContactCreatedEvent> message,
    IMongoDatabase mongoDatabase,
    string mailServerName,
    int smtpPort,
    DateTime currentDate,
    CancellationToken cancellationToken = default)
  {
    var messageColl = GetMessageCollection(mongoDatabase);

    await NotifyAddedToAgendaAsync(
      message,
      "dragos.tudor@gmail.com",
      currentDate,
      (messageIdempotency, cancellationToken) => FindMessageDuplication(messageColl.AsQueryable(), messageIdempotency).FirstOrDefaultAsync(cancellationToken) as Task<Message?>,
      (notification, cancellationToken) => SendNotificationAsync(notification, mailServerName, smtpPort, cancellationToken),
      (message, cancellationToken) => InsertMessageAsync(messageColl, message, cancellationToken),
      cancellationToken
    );

    return default;
  }
}
