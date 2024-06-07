
namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  public static TSuccess? FromSuccess<TSuccess, TFailure> (Result<TSuccess?, TFailure?> result) => result;

  public static TFailure? FromFailure<TSuccess, TFailure> (Result<TSuccess?, TFailure?> result) => result;
}

partial class ExtensionsFuncs
{
  public static Exception[] ToArray<T> (IEnumerable<T> values) where T: Exception => values.ToArray();
}