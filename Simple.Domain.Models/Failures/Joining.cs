
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static string JoinFailures<T> (IEnumerable<T> errors) where T: Failure => string.Join(Environment.NewLine, errors.Select(error => error.Message));
}