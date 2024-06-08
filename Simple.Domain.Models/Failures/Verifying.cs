
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static bool ExistFailure<T> (T? error) where T: Failure => error is not null;

  public static bool ExistsFailures<T> (IEnumerable<T?> errors) where T: Failure  => errors.Any(ExistFailure);
}