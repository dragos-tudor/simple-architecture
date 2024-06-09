
using System.Threading.Channels;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  public static ValueTask<TMessage> DequeueMessage<TMessage> (Channel<TMessage> queue, CancellationToken cancellationToken = default) =>
    queue.Reader.ReadAsync(cancellationToken);

  public static async Task DequeueMessages<TMessage> (
    Channel<TMessage> queue,
    HandleMessage<TMessage> handleMessage,
    Action<TMessage?, Exception> logError,
    CancellationToken cancellationToken= default)
  {
    while(!cancellationToken.IsCancellationRequested)
    {
      TMessage? message = default;
      try {
        message = await DequeueMessage(queue, cancellationToken);
        await handleMessage(message, cancellationToken);
      }
      catch (OperationCanceledException) { return; }
      catch (Exception ex) { logError(message, ex); }
    }
  }
}