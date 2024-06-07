
using System.Threading.Channels;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  public static bool ProduceMessage<TMessage> (Channel<TMessage> queue, TMessage message) => EnqueueMessage(message, queue);
}