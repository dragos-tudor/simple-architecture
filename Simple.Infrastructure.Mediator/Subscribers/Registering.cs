
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static Result<Subscriber<TMessage, TFailure>[]?, string[]?> RegisterSubscriber<TMessage, TFailure> (
    Subscriber<TMessage, TFailure> subscriber,
    Subscriber<TMessage, TFailure>[] subscribers)
  =>
    ValidateSubscriber(subscribers, subscriber).Where(ExistValidationError).ToArray() switch {
      [] => AppendSubscriber(subscribers, subscriber),
      var errors => errors!
    };
}