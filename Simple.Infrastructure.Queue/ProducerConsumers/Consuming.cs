
using System.Threading.Channels;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  public static Task ConsumeMessages<TMessage> (
    Channel<TMessage> queue,
    Func<TMessage, CancellationToken, Task<bool>> handleMessage,
    Func<TMessage, CancellationToken, Task> finalizeMessage,
    CancellationToken cancellationToken = default)
  =>
    DequeueMessages (
      queue,
      async (message, cancellationToken) => {
        if(!await handleMessage(message, cancellationToken)) return;

        LogFinalizingMessage(Logger, GetMessageType(message), GetMessageTraceId(message));
        await finalizeMessage(message, cancellationToken);
      },
      (TMessage? message, Exception ex) => LogConsumingMessageError(Logger, GetMessageType(message), GetMessageTraceId(message), ex.Message, ex.StackTrace),
      cancellationToken
    );

  public static async Task<bool> ConsumeMessage<TMessage, TFailure> (
    TMessage message,
    Func<TMessage, CancellationToken, Task<IEnumerable<TFailure>>> handleMessage,
    CancellationToken cancellationToken)
  {
    LogHandlingMessage(Logger, GetMessageType(message), GetMessageTraceId(message));
    var handleFailures = handleMessage(message, cancellationToken);

    return !(await handleFailures)
      .Select(failure => { LogHandledMessageError(Logger, GetMessageType(message), GetMessageTraceId(message), failure!.ToString()!); return failure; })
      .Any();
  }
}