
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  internal static Result<TSuccess?, TFailure?> CreateFailureResult<TSuccess, TFailure> (TFailure failure) => new () { Failure = failure };

  internal static Result<TSuccess?, TFailure?> CreateSuccessResult<TSuccess, TFailure> (TSuccess success) => new () { Success = success };
}