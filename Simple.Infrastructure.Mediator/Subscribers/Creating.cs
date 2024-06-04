
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  internal static Subscriber<TMessage> CreateSubscriber<TMessage> (string subscriberId, Func<TMessage, CancellationToken, Task<string?>> messageHandler)=>
    CreateSubscriber<TMessage, TMessage> (subscriberId, messageHandler);

  public static Subscriber<TMessage> CreateSubscriber<TMessage, TPayload> (string subscriberId, Func<TMessage, CancellationToken, Task<string?>> messageHandler)=>
    new (subscriberId, GetMessageType<TPayload>(), messageHandler);
}