
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static TSuccess? FromSuccess<TSuccess, TError>(Result<TSuccess?, TError?> result) => result;

  public static TError? FromError<TSuccess, TError>(Result<TSuccess?, TError?> result) => result;
}