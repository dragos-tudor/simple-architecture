
namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static Task ConsumeMessages<TMessage> (
    Channel<TMessage> queue,
    IEnumerable<Subscriber<TMessage>> subscribers,
    Func<TMessage, CancellationToken, Task> finalizeMessage,
    CancellationToken cancellationToken = default)
  =>
    DequeueMessages (
      queue,
      async (message, cancellationToken) => {
        if(!await HandleMessage(message, subscribers, cancellationToken)) return;

        LogFinalizingMessage(Logger, GetMessageType(message), GetMessageTraceId(message));
        await finalizeMessage(message, cancellationToken);
      },
      (TMessage? message, Exception ex) => LogConsumingMessageError(Logger, GetMessageType(message), GetMessageTraceId(message), ex.Message, ex.StackTrace),
      cancellationToken
    );
}