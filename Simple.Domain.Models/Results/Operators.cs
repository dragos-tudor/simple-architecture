#pragma warning disable CA2225

namespace Simple.Domain.Models;

public partial record Result<TSuccess, TFailure>
{
  public static implicit operator TFailure (Result<TSuccess?, TFailure?> result) => result.Failure!;

  public static implicit operator TSuccess (Result<TSuccess?, TFailure?> result) => result.Success!;

  public static implicit operator Result<TSuccess?, TFailure?> (TFailure failure) => CreateFailureResult<TSuccess, TFailure>(failure);

  public static implicit operator Result<TSuccess?, TFailure?> (TSuccess success) => CreateSuccessResult<TSuccess, TFailure>(success);
}