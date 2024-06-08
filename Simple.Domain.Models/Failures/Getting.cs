
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static IEnumerable<T> GetFailures<T> (IEnumerable<T?> errors) where T: Failure => errors.Where(ExistFailure)!;
}