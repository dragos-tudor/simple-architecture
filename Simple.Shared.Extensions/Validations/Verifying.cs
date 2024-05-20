
namespace Simple.Shared.Extensions;

partial class SharingFuncs
{
  public static bool ExistValidationError (string? error) => !IsNullOrEmpty(error);
}