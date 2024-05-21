
namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  public static bool ExistValidationError (string? error) => !IsNullOrEmpty(error);
}