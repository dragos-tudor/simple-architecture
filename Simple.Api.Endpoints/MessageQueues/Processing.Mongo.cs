
using MongoDB.Driver;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  internal static Task ProcessMessageMongoAsync(Channel<Message> messageQueue, IMongoDatabase mongoDatabase, byte maxErrors, ILogger logger, CancellationToken cancellationToken = default) =>
    ProcessMessagesAsync(
      messageQueue,
      async (message, cancellationToken) =>
      {
        await FinalizeMessageMongoAsync(message, mongoDatabase, cancellationToken);
      },
      async (message, exception, cancellationToken) =>
      {
        await HandleMessageErrorMongoAsync(message!, exception, maxErrors, mongoDatabase, cancellationToken);
      },
      logger,
      cancellationToken
    );
}