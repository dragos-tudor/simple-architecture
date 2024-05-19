
namespace Simple.Architecture.Sharing;

partial class SharingFuncs
{
  public static bool ExistValidationError (string? error) => !IsNullOrEmpty(error);
}