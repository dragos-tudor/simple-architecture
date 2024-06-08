
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static async Task<IEnumerable<TFailure>> HandleMessage<TMessage, TFailure> (
    TMessage message,
    string messageType,
    IEnumerable<Subscriber<TMessage, TFailure>> subscribers,
    CancellationToken cancellationToken = default)
  =>
    (await Task.WhenAll(DispatchMessage(message, messageType, subscribers, cancellationToken)))
      .Where(failure => failure is not null)!;
}