
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  static bool IsDuplicateSubscriber<TMessage> (IEnumerable<Subscriber<TMessage>> subscribers, Subscriber<TMessage> subscriber) => subscribers.Any(subs => subs.SubscriberId == subscriber.SubscriberId);

  static bool IsMissingSubscriberMessageType (string messageType) => string.IsNullOrEmpty(messageType);

  static bool IsMissingSubscriberId (string subscriberId) => string.IsNullOrEmpty(subscriberId);
}