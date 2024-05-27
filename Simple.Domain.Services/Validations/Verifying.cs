
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static bool ExistValidationError (string? error) => !string.IsNullOrEmpty(error);

  public static bool ExistsValidationErrors (IEnumerable<string?> errors) => errors.Any(ExistValidationError);
}