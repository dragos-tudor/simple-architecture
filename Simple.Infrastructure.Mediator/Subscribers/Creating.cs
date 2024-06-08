
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static Subscriber<TMessage, TFailure> CreateSubscriber<TMessage, TPayload, TFailure> (string subscriberId, Func<TMessage, CancellationToken, Task<TFailure?>> messageHandler)=>
    new (subscriberId, GetMessageType<TPayload>(), messageHandler);
}