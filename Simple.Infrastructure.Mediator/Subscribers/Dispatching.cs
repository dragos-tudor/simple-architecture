
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static IEnumerable<Task<string?>> DispatchMessage<TMessage> (
    TMessage message,
    string messageType,
    IEnumerable<Subscriber<TMessage>> subscribers,
    CancellationToken cancellationToken = default)
  {
    foreach (var subscriber in FindSubscribers(subscribers, messageType))
      yield return subscriber.MessageHandler(message, cancellationToken);
  }
}