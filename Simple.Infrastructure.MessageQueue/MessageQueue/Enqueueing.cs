
using System.Threading.Channels;

namespace Simple.Infrastructure.MessageQueue;

partial class MessageQueueFuncs
{
  public static bool EnqueueMessage<TMessage>(Channel<TMessage> queue, TMessage workItem) => queue.Writer.TryWrite(workItem);
}