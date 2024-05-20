
namespace Simple.Shared.Extensions;

partial class SharingFuncs
{
  public static bool IsEmptyCollection<T> (IEnumerable<T> items) => items.Any();
}