
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  static bool IsDuplicateSubscriber<TMessage, TFailure> (IEnumerable<Subscriber<TMessage, TFailure>> subscribers, Subscriber<TMessage, TFailure> subscriber) => subscribers.Any(subs => subs.SubscriberId == subscriber.SubscriberId);

  static bool IsMissingSubscriberMessageType (string messageType) => IsNullOrEmpty(messageType);

  static bool IsMissingSubscriberId (string subscriberId) => IsNullOrEmpty(subscriberId);
}