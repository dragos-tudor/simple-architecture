
using System.Threading.Channels;
using Simple.Infrastructure.Queue;

namespace Simple.Web.Endpoints;

partial class EndpointsFuncs
{
  public static Func<CancellationToken, Task> ConsumeMessages<TMessage>
    (Channel<TMessage> queue, IEnumerable<Subscriber<TMessage>> subscribers) => (CancellationToken cancellationToken) =>
      QueueFuncs.ConsumeMessages (queue, (message, cancellationToken) => HandleMessage(message, subscribers, cancellationToken), cancellationToken);
}