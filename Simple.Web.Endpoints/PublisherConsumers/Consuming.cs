
using System.Threading.Channels;

namespace Simple.Web.Endpoints;

partial class EndpointsFuncs
{
  public static Func<CancellationToken, Task> ConsumeMessages<TMessage> (Channel<TMessage> queue, IEnumerable<Subscriber<TMessage>> subscribers, Func<TMessage, CancellationToken, Task<bool>> finalizeMessage) => (CancellationToken cancellationToken) =>
    DequeueMessages (
      queue,
      async (message, cancellationToken) =>
        await HandleMessage(message, subscribers, cancellationToken) &&
        await finalizeMessage(message, cancellationToken)
      ,
      (TMessage? message, Exception ex) => LogConsumeMessageError(Logger, GetMessageType(message), GetMessageTraceId(message), ex.Message),
      cancellationToken
    );
}