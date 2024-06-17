
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

        LogHandlingMessage(logger, GetMessageId(message), GetMessageType(message), GetMessageCorrelationId(message));
        await handleMessage(message);
      }
      catch (OperationCanceledException) {
        LogEndConsumingMessages(logger);
        return;
      }
      catch (Exception ex) {
        try {
          LogHandledMessageError(logger, GetMessageId(message), GetMessageType(message), GetMessageCorrelationId(message), ex.Message, ex.StackTrace);
          await handleMessageError(message, ex);
        }
        catch(Exception innerEx) {
          LogHandlingMessageError(logger, GetMessageId(message), GetMessageType(message), GetMessageCorrelationId(message), innerEx.Message, innerEx.StackTrace);
        }
      }
    }
  }
}