
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static bool ExistsFailure<T> (T? error) where T: Failure => error is not null;

  public static bool ExistFailures<T> (IEnumerable<T?> errors) where T: Failure  => errors.Any(ExistsFailure);
}