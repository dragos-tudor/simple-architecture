#pragma warning disable CA1851

namespace Simple.Web.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<bool> HandleMessage<TMessage> (TMessage message, IEnumerable<Subscriber<TMessage>> subscribers, CancellationToken cancellationToken)
  {
    var dispatchResult = DispatchMessage(message, GetMessageType(message), subscribers, cancellationToken)!;
    await Task.WhenAll(dispatchResult, cancellationToken);

    var dispatchErrors = GetDispatchErrors(dispatchResult)!;
    foreach(var error in dispatchErrors) LogDispatchMessageError(Logger, GetMessageType(message), error);

    return dispatchErrors.Any();
  }
}