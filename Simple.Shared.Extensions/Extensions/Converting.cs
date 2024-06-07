
namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  public static T[] ToArray<T> (IEnumerable<T> values) => values.ToArray();
}