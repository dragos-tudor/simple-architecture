
namespace Simple.Domain.Api;

public delegate bool ProduceMessage<TMessage> (TMessage message) where TMessage: Message;

public delegate Task SaveModel<TModel> (TModel model, CancellationToken cancellationToken = default);

public delegate Task SaveModels<TModel1, TModel2> (TModel1 model1, TModel2 model2, CancellationToken cancellationToken = default);

public delegate Task SendNotification<TModel> (TModel model, CancellationToken cancellationToken = default);