
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  public static IEnumerable<string> GetValidationErrors (IEnumerable<string?> errors) =>
    errors.Where(ExistValidationError).Select(error => error!);
}