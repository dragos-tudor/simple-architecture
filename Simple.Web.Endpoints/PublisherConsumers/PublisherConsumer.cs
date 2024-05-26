
using System.Threading.Channels;

namespace Simple.Web.Endpoints;

public class PublisherConsumer<TMessage> (Channel<TMessage> channel, IEnumerable<Subscriber<TMessage>> subscribers)
{
  public bool PublishMessage (TMessage message) => EnqueueMessage(message, channel);

  public Func<CancellationToken, Task> ConsumeMessage => ConsumeMessages(channel, subscribers);
}