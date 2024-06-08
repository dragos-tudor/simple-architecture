
namespace Simple.Infrastructure.Queue;

public delegate Task FinalizeMessage<TMessage> (TMessage message, CancellationToken cancellationToken);

public delegate Task HandleMessage<TMessage> (TMessage message, CancellationToken cancellationToken);

public delegate Task<IEnumerable<TFailure>> HandleMessage<TMessage, TFailure> (TMessage message, CancellationToken cancellationToken);
