
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static IEnumerable<Task<string?>> PublishMessage<TMessage, TPayload> (TMessage message, IEnumerable<Subscriber<TMessage>> subscribers, CancellationToken cancellationToken = default) =>
    DispatchMessage(message, GetMessageType<TPayload>(), subscribers, cancellationToken);

  public static IEnumerable<Task<string?>> PublishMessage<TMessage> (TMessage message, IEnumerable<Subscriber<TMessage>> subscribers, CancellationToken cancellationToken = default) =>
    PublishMessage<TMessage, TMessage> (message, subscribers, cancellationToken);
}