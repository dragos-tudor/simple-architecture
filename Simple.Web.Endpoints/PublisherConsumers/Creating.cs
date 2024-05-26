
using System.Threading.Channels;

namespace Simple.Web.Endpoints;

partial class EndpointsFuncs
{
  public static PublisherConsumer<TMessage> CreatePublisherConsumer<TMessage> (Channel<TMessage> queue, IEnumerable<Subscriber<TMessage>> subscribers) =>
    new (queue, subscribers);
}