
using System.Threading;

namespace Simple.Domain.Services;

public delegate Task<TResult> FindModel<TModel, TResult> (TModel model, CancellationToken cancellationToken = default);

public delegate Task<IEnumerable<TResult>> FindModels<TModel, TResult> (IEnumerable<TModel> models, CancellationToken cancellationToken = default);