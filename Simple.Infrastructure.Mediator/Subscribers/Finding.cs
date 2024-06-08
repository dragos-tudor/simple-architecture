
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  internal static IEnumerable<Subscriber<TMessage, TFailure>> FindSubscribers<TMessage, TFailure> (IEnumerable<Subscriber<TMessage, TFailure>> subscribers, string messageType)=>
    subscribers.Where(subscriber => subscriber.MessageType == messageType);
}