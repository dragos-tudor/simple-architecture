
namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static bool ProduceMessage<TMessage> (Channel<TMessage> queue, TMessage message) =>
    EnqueueMessage(message, queue);
}