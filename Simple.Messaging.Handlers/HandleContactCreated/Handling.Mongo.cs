
using MongoDB.Driver;

namespace Simple.Messaging.Handlers;

partial class HandlersFuncs
{
  public static Task<NotificationSentEvent?> HandleContactCreatedMongoAsync(
    Message<ContactCreatedEvent> message,
    IMongoDatabase mongoDatabase,
    MailServerOptions mailServerOptions,
    DateTime currentDate,
    CancellationToken cancellationToken = default)
  {
    var messageColl = GetMessageCollection(mongoDatabase);

    return HandleContactCreatedAsync(
      message,
      "dragos.tudor@gmail.com",
      currentDate,
      (messageIdempotency, cancellationToken) => FindMessageDuplication(messageColl.AsQueryable(), messageIdempotency).FirstOrDefaultAsync(cancellationToken) as Task<Message?>,
      (@event, cancellationToken) => SendMailMessageAsync(ToMimeMessage(@event), mailServerOptions, cancellationToken),
      (message, cancellationToken) => InsertMessageAsync(messageColl, message, cancellationToken),
      cancellationToken
    );
  }
}
