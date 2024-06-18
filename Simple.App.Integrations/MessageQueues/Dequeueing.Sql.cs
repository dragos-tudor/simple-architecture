
namespace Simple.App.Integrations;

partial class IntegrationFuncs
{
  internal static Task DequeueSqlMessages (Channel<Message> messageQueue, Subscriber<Message>[] subscribers, AgendaContextFactory agendaContextFactory, MessageHandlerOptions handlerOptions, ILogger logger, CancellationToken queueCancellationToken = default) =>
    DequeueMessages(
      messageQueue,
      async (message) => {
        using var cancellationTokenSource = new CancellationTokenSource(handlerOptions.HandleTimeout);
        var cancellationToken = cancellationTokenSource.Token;

        await HandleMessageParallel(message, GetMessageType(message)!, subscribers, cancellationToken);
        await FinalizeSqlMessage(message, agendaContextFactory, cancellationToken);
      },
      async (message, exception) => {
        using var cancellationTokenSource = new CancellationTokenSource(handlerOptions.HandleTimeout);
        var cancellationToken = cancellationTokenSource.Token;
        if(!ExistMessage(message)) return;

        await HandleErrorSqlMessage(message!, exception, agendaContextFactory, handlerOptions, cancellationToken);
      },
      logger,
      queueCancellationToken
    );
}