
namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  internal static Task DequeueSqlMessages (Channel<Message> messageQueue, Subscriber<Message>[] subscribers, AgendaContextFactory sqlContextFactory, MessageHandlerOptions handlerOptions, ILogger logger, CancellationToken queueCancellationToken = default) =>
    DequeueMessages(
      messageQueue,
      async (message) => {
        using var cancellationTokenSource = new CancellationTokenSource(handlerOptions.HandleTimeout);
        var cancellationToken = cancellationTokenSource.Token;

        await HandleMessageParallel(message, GetMessageType(message)!, subscribers, cancellationToken);
        await FinalizeSqlMessage(message, sqlContextFactory, cancellationToken);
      },
      async (message, exception) => {
        using var cancellationTokenSource = new CancellationTokenSource(handlerOptions.HandleTimeout);
        var cancellationToken = cancellationTokenSource.Token;

        await HandleErrorSqlMessage(message!, exception, sqlContextFactory, handlerOptions.MaxFailures, cancellationToken);
      },
      logger,
      queueCancellationToken
    );
}