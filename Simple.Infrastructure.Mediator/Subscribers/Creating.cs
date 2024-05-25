
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static Subscriber<TMessage> CreateSubscriber<TMessage>(
    string subscriberId,
    string messageType,
    Func<TMessage, CancellationToken, Task<string>> messageHandler)
  =>
    new (subscriberId, messageType, messageHandler);

  public static Subscriber<Message> CreateSubscriber<TPayload>(
    string subscriberId,
    Func<Message<TPayload>, CancellationToken, Task<string>> messageHandler)
  =>
    CreateSubscriber<Message> (subscriberId, GetTypeName<TPayload>(),
      (message, cancellationToken) => messageHandler((Message<TPayload>)message, cancellationToken));
}