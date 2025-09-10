#pragma warning disable IDE0023

namespace Simple.Domain.Services;

public partial record Result<TSuccess, TError>
{
  public static implicit operator TError(Result<TSuccess?, TError?> result) => result.Error!;

  public static implicit operator TSuccess(Result<TSuccess?, TError?> result) => result.Success!;

  public static implicit operator Result<TSuccess?, TError?>(TError error) => CreateErrorResult<TSuccess, TError>(error);

  public static implicit operator Result<TSuccess?, TError?>(TSuccess success) => CreateSuccessResult<TSuccess, TError>(success);
}