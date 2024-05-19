
using System.Threading.Channels;

namespace Simple.Architecture.Queue;

partial class QueueFuncs
{
  public static Channel<TMessage> CreateMessageQueue<TMessage> (int capacity, BoundedChannelFullMode fullMode = BoundedChannelFullMode.DropWrite) =>
    Channel.CreateBounded<TMessage>(
      new BoundedChannelOptions(capacity) {FullMode = fullMode}
    );
}