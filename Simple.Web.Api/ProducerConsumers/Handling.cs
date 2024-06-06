
namespace Simple.Web.Api;

partial class ApiFuncs
{
  // TODO: dispatch using parralel|serial strategy
  public static async Task<bool> HandleMessage<TMessage> (TMessage message, IEnumerable<Subscriber<TMessage>> subscribers, CancellationToken cancellationToken)
  {
    LogHandlingMessage(Logger, GetMessageType(message), GetMessageTraceId(message));
    var handleErrors = MediatorFuncs.HandleMessage(message, GetMessageType(message)!, subscribers, cancellationToken);

    return (await Task.WhenAll(handleErrors))
      .Where(ExistHandleError)
      .Select(error => { LogHandledMessageError(Logger, GetMessageType(message), GetMessageTraceId(message), error!); return error; })
      .Any();
  }
}