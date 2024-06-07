
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  internal static Subscriber<TMessage> CreateSubscriber<TMessage> (string subscriberId, Func<TMessage, CancellationToken, Task<Exception?>> messageHandler)=>
    CreateSubscriber<TMessage, TMessage> (subscriberId, messageHandler);

  public static Subscriber<TMessage> CreateSubscriber<TMessage, TPayload> (string subscriberId, Func<TMessage, CancellationToken, Task<Exception?>> messageHandler)=>
    new (subscriberId, GetMessageType<TPayload>(), messageHandler);
}