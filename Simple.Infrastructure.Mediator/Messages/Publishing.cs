
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static Task<IEnumerable<Exception>> PublishMessage<TMessage, TPayload> (TMessage message, IEnumerable<Subscriber<TMessage>> subscribers, CancellationToken cancellationToken = default) =>
    HandleMessage(message, GetMessageType<TPayload>(), subscribers, cancellationToken);

  public static Task<IEnumerable<Exception>> PublishMessage<TMessage> (TMessage message, IEnumerable<Subscriber<TMessage>> subscribers, CancellationToken cancellationToken = default) =>
    PublishMessage<TMessage, TMessage> (message, subscribers, cancellationToken);
}