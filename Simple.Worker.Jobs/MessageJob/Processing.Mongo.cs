
using MongoDB.Driver;

namespace Simple.Worker.Jobs;

partial class JobsFuncs
{
  public static Task ProcessMessageMongoAsync(
    IMongoDatabase mongoDatabase,
    byte maxErrors,
    MessageJobOptions jobOptions,
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
        // process messages here
        await FinalizeMessageMongoAsync(message, mongoDatabase, cancellationToken);
      },
      (message, exception, cancellationToken) =>
        HandleMessageErrorMongoAsync(message!, exception, maxErrors, mongoDatabase, cancellationToken),
      jobOptions,
      timeProvider,
      logger,
      cancellationToken
    );
}