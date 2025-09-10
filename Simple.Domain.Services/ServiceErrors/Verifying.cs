
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  static bool ExistsError(string? error) => !string.IsNullOrEmpty(error);

  public static bool ExistErrors(IEnumerable<string?> errors) => errors.Any(ExistsError);
}