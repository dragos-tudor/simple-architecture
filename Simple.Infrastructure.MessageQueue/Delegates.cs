
namespace Simple.Infrastructure.MessageQueue;

public delegate Task ProcessMessage<TMessage>(TMessage message, CancellationToken cancellationToken = default);

public delegate Task HandleMessageError<TMessage>(TMessage? message, Exception exception, CancellationToken cancellationToken = default);
