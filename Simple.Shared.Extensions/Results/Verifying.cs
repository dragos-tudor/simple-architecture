
namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  public static bool IsFailureResult<TSuccess, TFailure> (Result<TSuccess, TFailure> result) =>  result.Failure is not null;

  public static bool IsSuccessResult<TSuccess, TFailure> (Result<TSuccess, TFailure> result) =>  result.Success is not null;
}