
namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  public static TSuccess? FromSuccess<TSuccess, TFailure> (Result<TSuccess?, TFailure?> result) => result;

  public static TFailure? FromFailure<TSuccess, TFailure> (Result<TSuccess?, TFailure?> result) => result;
}