
namespace Simple.Domain.Services;

public delegate Task<TResult> FindModel<TModel, TResult> (TModel model);

public delegate Task<IEnumerable<TResult>> FindModels<TModel, TResult> (IEnumerable<TModel> models);

public delegate Task SaveModel<TModel> (TModel model);

public delegate Task SaveModels<TModel1, TModel2> (TModel1 model1, TModel2 model2);

public delegate Task SaveModelAndEvent<TModel, TEvent> (TModel model, TEvent @event);

public delegate Task PublishEvent<TEvent> (TEvent @event);