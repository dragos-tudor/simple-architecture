#pragma warning disable CA1031

using System.Threading.Channels;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  public static async Task ConsumeMessages<TMessage> (
    Channel<TMessage> queue,
    Func<TMessage, CancellationToken, Task<string>> messageHandler,
    CancellationToken cancellationToken)
  {
    while(!cancellationToken.IsCancellationRequested)
    {
      try {
        var message = await DequeueMessage(queue, cancellationToken);
        await messageHandler(message, cancellationToken);
      }
      catch (OperationCanceledException) { LogConsumerCanceledError(Logger); }
      catch (Exception ex) { LogConsumerError(Logger, ex.Message); }
    }
  }
}