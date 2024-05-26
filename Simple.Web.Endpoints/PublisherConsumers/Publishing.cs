
using System.Threading.Channels;

namespace Simple.Web.Endpoints;

partial class EndpointsFuncs
{
  public static Func<TMessage, bool> PublishMessage<TMessage> (Channel<TMessage> queue) => (TMessage message) =>
    EnqueueMessage(message, queue);
}