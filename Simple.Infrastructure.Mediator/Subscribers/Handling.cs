
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static async Task<IEnumerable<Exception>> HandleMessage<TMessage> (TMessage message, string messageType, IEnumerable<Subscriber<TMessage>> subscribers, CancellationToken cancellationToken = default) =>
    (await Task.WhenAll(
      FindSubscribers(subscribers, messageType)
        .Select(subscriber => subscriber.MessageHandler(message, cancellationToken))
    ))
    .Where(ExistError)!;
}