
namespace Simple.Infrastructure.JobScheduler;

public delegate Task<List<TModel>> QueryModels<TModel, TQueryable> (TQueryable query, CancellationToken cancellationToken = default) where TQueryable: IQueryable<TModel>;

public delegate bool ResumeMessage<TMessage> (TMessage message) where TMessage: Message;