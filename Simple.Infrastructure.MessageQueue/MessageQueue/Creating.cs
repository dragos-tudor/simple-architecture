
using System.Threading.Channels;

namespace Simple.Infrastructure.MessageQueue;

partial class MessageQueueFuncs
{
  static BoundedChannelOptions CreateBoundedChannelOptions(int capacity, BoundedChannelFullMode fullMode) => new(capacity) { FullMode = fullMode };

  public static Channel<TMessage> CreateMessageQueue<TMessage>(int capacity = 1000, BoundedChannelFullMode fullMode = BoundedChannelFullMode.DropWrite) =>
    Channel.CreateBounded<TMessage>(CreateBoundedChannelOptions(capacity, fullMode));
}