
namespace Simple.Architecture.Mediator;

public class Mediator
{
  Subscriber<Message>[] Subscribers = [];

  public virtual IEnumerable<Task<string>> Publish<TPayload> (Message<TPayload> message, CancellationToken cancellationToken = default) =>
    PublishMessage(message, Subscribers, cancellationToken);

  public virtual string[]? Subscribe<TPayload> (string subscriberId, Func<Message<TPayload>, CancellationToken, Task<string>> messageHandler)
  {
    var result = RegisterSubscriber(subscriberId, messageHandler, Subscribers);
    if(IsFailureResult(result)) return result;

    Subscribers = FromSuccess(result)!;
    return result;
  }

  public virtual bool Unsubscribe<TPayload> (string subscriberId)
  {
    Subscribers = UnregisterSubscriber(subscriberId, Subscribers);
    return true;
  }
}