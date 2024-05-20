
namespace Simple.Infrastructure.Queue;

public delegate Task<string> MessageHandler<TMessage>(TMessage message, CancellationToken cancellationToken);