
using System.Collections;

namespace Simple.Infrastructure.Queue;

public delegate Task<bool> MessageHandler<TMessage>(TMessage message, CancellationToken cancellationToken);