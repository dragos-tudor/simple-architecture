
using System.Threading.Channels;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  public static bool EnqueueMessage<TMessage> (Channel<TMessage> queue, TMessage workItem) => queue.Writer.TryWrite(workItem);
}