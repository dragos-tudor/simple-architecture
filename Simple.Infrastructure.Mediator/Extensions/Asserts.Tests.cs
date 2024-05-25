
namespace Simple.Infrastructure.Mediator;

partial class MediatorTests
{
  static void AreEqual<T>(
    IEnumerable<T> expected,
    IEnumerable<T>? actual)
  =>
    CollectionAssert.AreEqual(expected.ToArray(), actual?.ToArray());
}