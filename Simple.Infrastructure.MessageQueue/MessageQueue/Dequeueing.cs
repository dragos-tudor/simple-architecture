
using System.Threading.Channels;

namespace Simple.Infrastructure.MessageQueue;

partial class MessageQueueFuncs
{
  public static ValueTask<TMessage> DequeueMessage<TMessage>(Channel<TMessage> queue, CancellationToken cancellationToken = default) =>
    queue.Reader.ReadAsync(cancellationToken);
}