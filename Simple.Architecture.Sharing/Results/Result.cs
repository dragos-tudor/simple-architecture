
namespace Simple.Architecture.Sharing;

public partial record Result<TSuccess, TFailure>
{
  internal TSuccess? Success { get; init; }
  internal TFailure? Failure { get; init; }
}
