
using System.Threading.Channels;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static Func<TMessage, bool> ProduceMessage<TMessage> (Channel<TMessage> queue) => (TMessage message) =>
    EnqueueMessage(message, queue);
}