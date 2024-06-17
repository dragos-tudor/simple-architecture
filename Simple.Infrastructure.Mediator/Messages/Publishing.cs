
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static Task PublishMessage<TMessage, TPayload> (TMessage message, IEnumerable<Subscriber<TMessage>> subscribers, CancellationToken cancellationToken = default) =>
    HandleMessageParallel(message, GetMessageType<TPayload>(), subscribers, cancellationToken);
}