
namespace Simple.Architecture.Mediator;

partial class MediatorFuncs
{
  public static Subscriber<TMessage>[] UnregisterSubscriber<TMessage>(
    string subscriberId,
    Subscriber<TMessage>[] subscribers)
  =>
    subscribers.Where(subscriber => subscriber.SubscriberId != subscriberId).ToArray();
}