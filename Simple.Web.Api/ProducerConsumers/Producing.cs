
using System.Threading.Channels;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static Func<TMessage, bool> ProducehMessage<TMessage> (Channel<TMessage> queue) => (TMessage message) =>
    EnqueueMessage(message, queue);
}