
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static Subscriber<TMessage, TFailure>[] UnregisterSubscriber<TMessage, TFailure> (string subscriberId, Subscriber<TMessage, TFailure>[] subscribers) =>
    subscribers.Where(subscriber => subscriber.SubscriberId != subscriberId).ToArray();
}