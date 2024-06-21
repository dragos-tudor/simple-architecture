
using MongoDB.Driver;

namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  internal static Task DequeueMongoMessages (Channel<Message> messageQueue, Subscriber<Message>[] subscribers, IMongoDatabase agendaDb, MessageHandlerOptions handlerOptions, ILogger logger, CancellationToken queueCancellationToken = default) =>
    DequeueMessages(
      messageQueue,
      async (message) => {
        using var cancellationTokenSource = new CancellationTokenSource(handlerOptions.HandleTimeout);
        var cancellationToken = cancellationTokenSource.Token;

        await HandleMessageParallel(message, GetMessageType(message)!, subscribers, cancellationToken);
        await FinalizeMongoMessage(message, agendaDb, cancellationToken);
      },
      async (message, exception) => {
        using var cancellationTokenSource = new CancellationTokenSource(handlerOptions.HandleTimeout);
        var cancellationToken = cancellationTokenSource.Token;

        await HandleErrorMongoMessage(message!, exception, agendaDb, handlerOptions.MaxFailures, cancellationToken);
      },
      logger,
      queueCancellationToken
    );
}