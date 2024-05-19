
namespace Simple.Architecture.Sharing;

partial class SharingFuncs
{
  public static IEnumerable<string> GetValidationErrors (IEnumerable<string?> errors) =>
    errors.Where(ExistValidationError).Select(error => error!);
}