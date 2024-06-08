
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  static Subscriber<TMessage, TFailure>[] AppendSubscriber<TMessage, TFailure> (Subscriber<TMessage, TFailure>[] subscribers, Subscriber<TMessage,TFailure> subscriber) =>
    [.. subscribers, subscriber];
}