
namespace Simple.Domain.Services;

public delegate Task<TResult> FindModel<TModel, TResult>(TModel model, CancellationToken cancellationToken = default);

public delegate Task<IEnumerable<TResult>> FindModels<TModel, TResult>(IEnumerable<TModel> models, CancellationToken cancellationToken = default);

public delegate Task StoreModel<TModel>(TModel model, CancellationToken cancellationToken = default);

public delegate Task StoreModels<TModel1, TModel2>(TModel1 model1, TModel2 model2, CancellationToken cancellationToken = default);

public delegate bool EnqueueMessage<TMessage>(TMessage message) where TMessage : Message;

public delegate Task SendNotification(NotificationSentEvent @event, CancellationToken cancellationToken = default);