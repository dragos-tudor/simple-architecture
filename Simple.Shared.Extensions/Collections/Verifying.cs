
namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  public static bool IsEmptyCollection<T> (IEnumerable<T> items) => items.Any();
}