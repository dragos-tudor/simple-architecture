
namespace Simple.App.Services;

public delegate Task ProduceMessage<TMessage> (TMessage message);

public delegate Task SaveModel<TModel> (TModel model, CancellationToken cancellationToken = default);

public delegate Task SaveModels<TModel1, TModel2> (TModel1 model1, TModel2 model2, CancellationToken cancellationToken = default);