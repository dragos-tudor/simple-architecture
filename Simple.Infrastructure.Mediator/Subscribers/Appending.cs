
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  static Subscriber<TMessage, TFailure>[] AppendSubscriber<TMessage, TFailure> (IEnumerable<Subscriber<TMessage, TFailure>> subscribers, Subscriber<TMessage,TFailure> subscriber) =>
    [.. subscribers, subscriber];
}