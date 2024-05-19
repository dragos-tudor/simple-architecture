
using System.Threading.Channels;

namespace Simple.Architecture.Queue;

partial class QueueFuncs
{
  public static ValueTask<TMessage> DequeueMessage<TMessage> (Channel<TMessage> queue, CancellationToken cancellationToken = default) =>
    queue.Reader.ReadAsync(cancellationToken);
}