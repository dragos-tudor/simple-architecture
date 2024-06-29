
using MongoDB.Driver;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  internal static Task DequeueMongoMessages (Channel<Message> messageQueue, Subscriber<Message>[] subscribers, IMongoDatabase mongoDatabase, MessageHandlerOptions handlerOptions, ILogger logger, CancellationToken queueCancellationToken = default) =>
    DequeueMessages(
      messageQueue,
      async (message) => {
        using var cancellationTokenSource = new CancellationTokenSource(handlerOptions.HandleTimeout);
        var cancellationToken = cancellationTokenSource.Token;

        await HandleMessageParallel(message, GetMessageType(message)!, subscribers, cancellationToken);
        await FinalizeMongoMessage(message, mongoDatabase, cancellationToken);
      },
      async (message, exception) => {
        using var cancellationTokenSource = new CancellationTokenSource(handlerOptions.HandleTimeout);
        var cancellationToken = cancellationTokenSource.Token;

        await HandleErrorMongoMessage(message!, exception, mongoDatabase, handlerOptions.MaxFailures, cancellationToken);
      },
      logger,
      queueCancellationToken
    );
}