
namespace Simple.Architecture.Sharing;

partial class SharingFuncs
{
  public static bool IsEmptyCollection<T> (IEnumerable<T> items) => items.Any();
}