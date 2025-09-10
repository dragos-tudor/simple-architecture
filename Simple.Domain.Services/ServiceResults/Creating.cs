
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  internal static Result<TSuccess?, TError?> CreateErrorResult<TSuccess, TError>(TError error) => new() { Error = error };

  internal static Result<TSuccess?, TError?> CreateSuccessResult<TSuccess, TError>(TSuccess success) => new() { Success = success };
}