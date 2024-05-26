
namespace Simple.Infrastructure.Mediator;

public class Mediator
{
  Subscriber<Message>[] Subscribers = [];

  public virtual IEnumerable<Task<string?>> Publish<TPayload> (Message<TPayload> message, CancellationToken cancellationToken = default)
  {
    var result = PublishMessage(message, Subscribers, cancellationToken);
    LogPublishedMessage(Logger, GetTypeName<TPayload>());
    return result;
  }

  public virtual string[]? Subscribe<TPayload> (string subscriberId, Func<Message<TPayload>, CancellationToken, Task<string?>> messageHandler)
  {
    var result = RegisterSubscriber(subscriberId, messageHandler, Subscribers);
    if(IsFailureResult(result)) LogSubscribingSubscriberError(Logger, subscriberId, Join(",", FromFailure(result)!));
    if(IsFailureResult(result)) return result;

    Subscribers = FromSuccess(result)!;
    LogSubscribedSubscriber(Logger, subscriberId);
    return result;
  }

  public virtual bool Unsubscribe<TPayload> (string subscriberId)
  {
    Subscribers = UnregisterSubscriber(subscriberId, Subscribers);
    LogUnsubscribedSubscriber(Logger, subscriberId);
    return true;
  }
}