
namespace Simple.Infrastructure.Mediator;

public record Subscriber<TMessage> (string SubscriberId, string MessageType, Func<TMessage, CancellationToken, Task> MessageHandler);