
namespace Simple.Shared.Testing;

partial class TestingFuncs
{
  public static T[] ToArray<T> (IEnumerable<T> values) => values.ToArray();
}