
namespace Simple.Domain.Services;

public partial record Result<TSuccess, TError>
{
  internal TSuccess? Success { get; init; }
  internal TError? Error { get; init; }

  public void Deconstruct(out TSuccess? success, out TError? error)
  {
    success = Success;
    error = Error;
  }
}
