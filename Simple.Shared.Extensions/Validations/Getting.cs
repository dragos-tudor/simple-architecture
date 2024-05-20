
namespace Simple.Shared.Extensions;

partial class SharingFuncs
{
  public static IEnumerable<string> GetValidationErrors (IEnumerable<string?> errors) =>
    errors.Where(ExistValidationError).Select(error => error!);
}