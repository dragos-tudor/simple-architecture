
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static Task<IEnumerable<TFailure>> PublishMessage<TMessage, TPayload, TFailure> (TMessage message, IEnumerable<Subscriber<TMessage,TFailure>> subscribers, CancellationToken cancellationToken = default) =>
    HandleMessage(message, GetMessageType<TPayload>(), subscribers, cancellationToken);
}