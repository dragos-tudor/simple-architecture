
namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  public static IEnumerable<T> GetErrors<T> (IEnumerable<T?> errors) where T: Exception => errors.Where(ExistError)!;
}