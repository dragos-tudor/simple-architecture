
namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  public static IEnumerable<string> GetValidationErrors (IEnumerable<string?> errors) =>
    errors.Where(ExistValidationError).Select(error => error!);
}