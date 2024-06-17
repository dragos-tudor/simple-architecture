
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  static Subscriber<TMessage>[] AppendSubscriber<TMessage> (IEnumerable<Subscriber<TMessage>> subscribers, Subscriber<TMessage> subscriber) =>
    [.. subscribers, subscriber];
}