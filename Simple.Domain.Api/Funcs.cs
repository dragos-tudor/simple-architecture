
namespace Simple.Domain.Api;

public delegate Task ProduceMessage<TModel> (Message<TModel> message);

public delegate Task SaveModel<TModel> (TModel model, CancellationToken cancellationToken = default);

public delegate Task SaveModelAndMessage<TModel1, TModel2> (TModel1 model1, Message<TModel2> model2, CancellationToken cancellationToken = default);

public delegate Task SaveModels<TModel1, TModel2> (TModel1 model1, TModel2 model2, CancellationToken cancellationToken = default);

public delegate Task SaveMessage<TModel> (Message<TModel> message, CancellationToken cancellationToken = default);

public delegate Task SendNotification<TModel> (TModel model, CancellationToken cancellationToken = default);