
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static IEnumerable<Task<string>> PublishMessage<TMessage> (
    TMessage message,
    string messageType,
    IEnumerable<Subscriber<TMessage>> subscribers,
    CancellationToken cancellationToken = default)
  {
    foreach (var subscriber in FindSubscribers(subscribers, messageType))
      yield return subscriber.MessageHandler(message, cancellationToken);
  }

  public static IEnumerable<Task<string>> PublishMessage<TPayload> (
    Message<TPayload> message,
    IEnumerable<Subscriber<Message>> subscribers,
    CancellationToken cancellationToken = default)
  =>
    PublishMessage(message, GetTypeName(typeof(TPayload)), subscribers, cancellationToken);
}