
using MongoDB.Driver;

namespace Simple.Worker;

partial class WorkerFuncs
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
      async (minDate, maxDate, batchSize, cancellationToken) =>
      {
        var messageColl = GetMessageCollection(mongoDatabase);
        return await FindPendingMessages(messageColl.AsQueryable(), minDate, maxDate, batchSize).ToListAsync(cancellationToken);
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