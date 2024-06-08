
namespace Simple.Infrastructure.Mediator;

public record Subscriber<TMessage, TFailure> (string SubscriberId, string MessageType, Func<TMessage, CancellationToken, Task<TFailure?>> MessageHandler);