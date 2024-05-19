
namespace Simple.Architecture.Mediator;

public record Subscriber<TMessage> (
  string SubscriberId,
  string MessageType,
  Func<TMessage, CancellationToken, Task<string>> MessageHandler
);