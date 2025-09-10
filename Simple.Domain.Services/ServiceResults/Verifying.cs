
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static bool IsErrorResult<TSuccess, TError>(Result<TSuccess, TError> result) => result.Error is not null;

  public static bool IsSuccessResult<TSuccess, TError>(Result<TSuccess, TError> result) => result.Success is not null;
}