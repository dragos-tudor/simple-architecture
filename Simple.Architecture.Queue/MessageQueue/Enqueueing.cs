
using System.Threading.Channels;

namespace Simple.Architecture.Queue;

partial class QueueFuncs
{
  public static bool EnqueueMessage<TMessage> (TMessage workItem, Channel<TMessage> queue) =>
    queue.Writer.TryWrite(workItem);
}