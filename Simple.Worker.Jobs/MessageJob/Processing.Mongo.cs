
using MongoDB.Driver;

namespace Simple.Worker.Jobs;

partial class JobsFuncs
{
  public static Task ProcessMessageMongoAsync(
    IMongoDatabase mongoDatabase,
    byte maxErrors,
    MessageJobOptions jobOptions,
    MailServerOptions mailServerOptions,
    TimeProvider timeProvider,
    ILogger logger,
    CancellationToken cancellationToken = default)
  =>
    ProcessMessagesAsync(
      (minDate, maxDate, batchSize, cancellationToken) =>
      {
        var messageColl = GetMessageCollection(mongoDatabase);
        return QueryPendingMessages(messageColl.AsQueryable(), minDate, maxDate, batchSize).ToListAsync(cancellationToken);
      },
      async (message, cancellationToken) =>
      {
        if (message is Message<ContactCreatedEvent>)
          await HandleContactCreatedMongoAsync((Message<ContactCreatedEvent>)message, mongoDatabase, mailServerOptions, timeProvider.GetUtcNow().DateTime, cancellationToken);

        await FinalizeMessageMongoAsync(mongoDatabase, message, cancellationToken);
      },
      (message, exception, cancellationToken) =>
        HandleMessageErrorMongoAsync(mongoDatabase, message!, exception, maxErrors, cancellationToken),
      jobOptions,
      timeProvider,
      logger,
      cancellationToken
    );
}