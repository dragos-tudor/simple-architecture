
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static Result<Subscriber<TMessage>[]?, string[]?> RegisterSubscriber<TMessage> (
    Subscriber<TMessage> subscriber,
    Subscriber<TMessage>[] subscribers)
  =>
    ValidateSubscriber(subscribers, subscriber).ToArray() switch {
      [] => AppendSubscriber(subscribers, subscriber),
      var errors => errors
    };

  public static Result<Subscriber<Message>[]?, string[]?> RegisterSubscriber<TPayload> (
    string subscriberId,
    Func<Message<TPayload>, CancellationToken, Task<string?>> messageHandler,
    Subscriber<Message>[] subscribers)
  =>
    RegisterSubscriber(CreateSubscriber(subscriberId, messageHandler), subscribers);
}