
using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  public static ValueTask<TMessage> DequeueMessage<TMessage> (Channel<TMessage> queue, CancellationToken cancellationToken = default) =>
    queue.Reader.ReadAsync(cancellationToken);

  public static async Task DequeueMessages<TMessage> (
    Channel<TMessage> queue,
    HandleMessage<TMessage> handleMessage,
    HandleMessageError<TMessage> handleMessageError,
    ILogger logger,
    CancellationToken cancellationToken = default)
  {
    LogStartConsumingMessages(logger);
    while(!cancellationToken.IsCancellationRequested)
    {
      TMessage? message = default;
      try {
        message = await DequeueMessage(queue, cancellationToken);

        LogHandleMessage(logger, GetMessageId(message), GetMessageType(message), GetMessageCorrelationId(message));
        await handleMessage(message);
      }
      catch (OperationCanceledException) { break; }
      catch (Exception exception) {
        var messageId = GetMessageId(message);
        var messageType = GetMessageType(message);
        var correlationId = GetMessageCorrelationId(message);
        try {
          LogHandleMessageError(logger, messageId, messageType, correlationId, exception.Message, exception.StackTrace);
          await handleMessageError(message, exception);
        }
        catch(Exception innerException) {
          LogHandleInnerMessageError(logger, messageId, messageType, correlationId, innerException.Message, innerException.StackTrace);
          if (innerException.InnerException is not null)
            LogHandleInnerMessageError(logger, messageId, messageType, correlationId, innerException.InnerException.Message, innerException.InnerException.StackTrace);
        }
      }
    }
    LogEndConsumingMessages(logger);
  }
}