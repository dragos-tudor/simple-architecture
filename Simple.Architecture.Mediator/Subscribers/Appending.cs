
namespace Simple.Architecture.Mediator;

partial class MediatorFuncs
{
  static Subscriber<TMessage>[] AppendSubscriber<TMessage> (
    Subscriber<TMessage>[] subscribers,
    Subscriber<TMessage> subscriber)
  =>
    [.. subscribers, subscriber];
}