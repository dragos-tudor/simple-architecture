
using System.Threading.Channels;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  public static ValueTask<TMessage> DequeueMessage<TMessage> (Channel<TMessage> queue, CancellationToken cancellationToken = default) =>
    queue.Reader.ReadAsync(cancellationToken);

  public static async Task DequeueMessages<TMessage> (
    Channel<TMessage> queue,
    MessageHandler<TMessage> messageHandler,
    Func<Exception, string> logError,
    CancellationToken cancellationToken= default)
  {
    while(!cancellationToken.IsCancellationRequested)
    {
      try {
        var message = await DequeueMessage(queue, cancellationToken);
        await messageHandler(message, cancellationToken);
      }
      catch (OperationCanceledException ex) { logError(ex); return; }
      catch (Exception ex) { logError(ex); }
    }
  }
}