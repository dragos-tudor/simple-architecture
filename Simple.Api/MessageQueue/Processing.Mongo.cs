
using MongoDB.Driver;

namespace Simple.Api;

partial class ApiFuncs
{
  public static Task ProcessMessageMongoAsync(
    Channel<Message> messageQueue,
    IMongoDatabase mongoDatabase,
    byte maxErrors,
    MailServerOptions mailServerOptions,
    TimeProvider timeProvider,
    ILogger logger,
    CancellationToken cancellationToken = default)
  =>
    ProcessMessagesAsync(
      messageQueue,
      async (message, cancellationToken) =>
      {
        if (message is Message<ContactCreatedEvent>)
          await HandleContactCreatedMongoAsync((Message<ContactCreatedEvent>)message, mongoDatabase, mailServerOptions, timeProvider.GetUtcNow().DateTime, cancellationToken);
        await FinalizeMessageMongoAsync(mongoDatabase, message, cancellationToken);
      },
      async (message, exception, cancellationToken) =>
        await HandleMessageErrorMongoAsync(mongoDatabase, message!, exception, maxErrors, cancellationToken)
      ,
      logger,
      cancellationToken
    );
}