
using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  public static Task ConsumeMessages<TMessage, TFailure> (
    Channel<TMessage> queue,
    HandleMessage<TMessage, TFailure> handleMessage,
    FinalizeMessage<TMessage> finalizeMessage,
    ILogger logger,
    CancellationToken cancellationToken = default)
  =>
    DequeueMessages (
      queue,
      async (message, cancellationToken) => {
        LogHandlingMessage(logger, GetMessageType(message), GetMessageTraceId(message));
        var failures = await handleMessage(message, cancellationToken);

        if(ExistsFailures(failures)) LogHandledMessageError(logger, GetMessageType(message), GetMessageTraceId(message), JoinFailures(failures));
        if(ExistsFailures(failures)) return;

        LogFinalizingMessage(logger, GetMessageType(message), GetMessageTraceId(message));
        await finalizeMessage(message, cancellationToken);
      },
      (TMessage? message, Exception ex) => LogConsumingMessageError(logger, GetMessageType(message), GetMessageTraceId(message), ex.Message, ex.StackTrace),
      cancellationToken
    );
}