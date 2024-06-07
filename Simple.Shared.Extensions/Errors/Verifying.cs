
namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  public static bool ExistError<T> (T? error) where T: Exception => error is not null;

  public static bool ExistsErrors<T> (IEnumerable<T?> errors) where T: Exception  => errors.Any(ExistError);
}