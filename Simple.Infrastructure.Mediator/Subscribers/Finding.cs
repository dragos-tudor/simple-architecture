
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  internal static IEnumerable<Subscriber<TMessage>> FindSubscribers<TMessage> (IEnumerable<Subscriber<TMessage>> subscribers, string messageType)=>
    subscribers.Where(subscriber => subscriber.MessageType == messageType);
}