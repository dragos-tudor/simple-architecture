
namespace Simple.Shared.Extensions;

partial class SharingFuncs
{
  public static TSuccess? FromSuccess<TSuccess, TFailure> (Result<TSuccess?, TFailure?> result) => result;

  public static TFailure? FromFailure<TSuccess, TFailure> (Result<TSuccess?, TFailure?> result) => result;
}

public partial record Result<TSuccess, TFailure>
{
  public TFailure ToTFailure() => Failure!;

  public TSuccess ToTSuccess() => Success!;

  public Result<TSuccess?, TFailure?> ToResult(TFailure failure) => CreateFailureResult<TSuccess, TFailure>(failure);

  public Result<TSuccess?, TFailure?> ToResult(TSuccess success) => CreateSuccessResult<TSuccess, TFailure>(success);
}