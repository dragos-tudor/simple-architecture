
using System.Threading.Channels;

namespace Simple.Web.Endpoints;

partial class EndpointsFuncs
{
  public static Func<TMessage, bool> CreatePublisher<TMessage> (Channel<TMessage> channel) =>
    PublishMessage(channel);

  public static Func<CancellationToken, Task> CreateConsumer<TMessage> (Channel<TMessage> channel, IEnumerable<Subscriber<TMessage>> subscribers, Func<TMessage, CancellationToken, Task<bool>> finalizeMessage) =>
    ConsumeMessages(channel, subscribers, finalizeMessage);
}