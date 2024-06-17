
using System.Threading.Channels;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  static BoundedChannelOptions CreateBoundedChannelOptions (int capacity, BoundedChannelFullMode fullMode) =>  new (capacity) {FullMode = fullMode};

  public static Channel<TMessage> CreateMessageQueue<TMessage> (int capacity = 1000, BoundedChannelFullMode fullMode = BoundedChannelFullMode.DropWrite) =>
    Channel.CreateBounded<TMessage>(CreateBoundedChannelOptions(capacity, fullMode));
}