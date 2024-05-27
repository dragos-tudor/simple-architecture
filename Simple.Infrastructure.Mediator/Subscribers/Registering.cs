
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static Result<Subscriber<TMessage>[]?, string[]?> RegisterSubscriber<TMessage> (
    Subscriber<TMessage> subscriber,
    Subscriber<TMessage>[] subscribers)
  =>
    ValidateSubscriber(subscribers, subscriber).Where(ExistValidationError).ToArray() switch {
      [] => AppendSubscriber(subscribers, subscriber),
      var errors => errors!
    };
}