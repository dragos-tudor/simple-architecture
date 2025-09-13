
namespace Simple.Infrastructure.JobScheduler;

public delegate Task<List<TModel>> FindMessages<TModel>(DateTime exclusiveMinDate, DateTime inclusiveMaxDate, int batchSize, CancellationToken cancellationToken = default);

public delegate Task ProcessMessage<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : Message;

public delegate Task HandleMessageError<TMessage>(TMessage message, Exception exception, CancellationToken cancellationToken = default);