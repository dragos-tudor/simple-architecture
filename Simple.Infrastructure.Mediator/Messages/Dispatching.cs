
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static IEnumerable<Task<TFailure?>> DispatchMessage<TMessage, TFailure> (
    TMessage message,
    string messageType,
    IEnumerable<Subscriber<TMessage, TFailure>> subscribers,
    CancellationToken cancellationToken = default)
  =>
    FindSubscribers(subscribers, messageType)
      .Select(subscriber => subscriber.MessageHandler(message, cancellationToken));
}