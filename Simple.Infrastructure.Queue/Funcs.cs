
namespace Simple.Infrastructure.Queue;

public delegate Task HandleMessage<TMessage> (TMessage message);

public delegate Task HandleMessageError<TMessage> (TMessage? message, Exception exeption);
