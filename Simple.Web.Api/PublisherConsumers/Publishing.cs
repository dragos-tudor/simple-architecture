
using System.Threading.Channels;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static Func<TMessage, bool> PublishMessage<TMessage> (Channel<TMessage> queue) => (TMessage message) =>
    EnqueueMessage(message, queue);
}