
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static Subscriber<TMessage> CreateSubscriber<TMessage, TPayload> (string subscriberId, Func<TMessage, CancellationToken, Task> messageHandler)=>
    new (subscriberId, GetMessageType<TPayload>(), messageHandler);
}