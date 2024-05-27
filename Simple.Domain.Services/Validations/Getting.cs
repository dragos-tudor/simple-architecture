
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static IEnumerable<string> GetValidationErrors (IEnumerable<string?> errors) =>
    errors.Where(ExistValidationError).Select(error => error!);
}