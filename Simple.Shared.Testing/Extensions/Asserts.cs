#pragma warning disable CA1851

namespace Simple.Shared.Testing;

partial class TestingFuncs
{
  public static void AreEqual<T> (IEnumerable<T> expected, IEnumerable<T> actual)
  {
    if(typeof(T).IsAssignableTo(typeof(Failure)))
      CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray(), new FailureComparer());

    if(!typeof(T).IsAssignableTo(typeof(Failure)))
      CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
  }

  public static void Contains<T> (IEnumerable<T> expected, IEnumerable<T> actual) =>
    CollectionAssert.Contains(expected.ToArray(), actual.ToArray());

  public static void DoesNotContain<T> (IEnumerable<T> expected, IEnumerable<T> actual) =>
    CollectionAssert.DoesNotContain(expected.ToArray(), actual.ToArray());
}