
namespace Simple.Infrastructure.Queue;

public delegate Task MessageHandler<TMessage>(TMessage message, CancellationToken cancellationToken);