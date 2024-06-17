
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static async Task HandleMessageParallel<TMessage> (
    TMessage message,
    string messageType,
    IEnumerable<Subscriber<TMessage>> subscribers,
    CancellationToken cancellationToken = default)
  {
    var tasks = FindSubscribers(subscribers, messageType).Select(subscriber => subscriber.MessageHandler(message, cancellationToken));
    await Task.WhenAll(tasks);
  }

  public static async Task HandleMessageSerial<TMessage> (
    TMessage message,
    string messageType,
    IEnumerable<Subscriber<TMessage>> subscribers,
    CancellationToken cancellationToken = default)
  {
    foreach(var messageSubscriber in FindSubscribers(subscribers, messageType))
      await messageSubscriber.MessageHandler(message, cancellationToken);
  }
}