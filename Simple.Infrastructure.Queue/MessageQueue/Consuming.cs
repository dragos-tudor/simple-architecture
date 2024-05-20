
using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  public static async Task ConsumeMessages<TMessage> (
    Channel<TMessage> queue,
    MessageHandler<TMessage> messageHandler,
    CancellationToken cancellationToken,
    ILogger? logger = default)
  {
    while(!cancellationToken.IsCancellationRequested)
    {
      try {
        var message = await DequeueMessage(queue, cancellationToken);
        LogConsumerDequeuedMessage(logger ?? Logger, message?.ToString());
        await messageHandler(message, cancellationToken);
      }
      catch (OperationCanceledException) { LogConsumerCanceledError(logger ?? Logger); }
      catch (Exception ex) { LogConsumerError(logger ?? Logger, ex.Message); }
    }
  }
}