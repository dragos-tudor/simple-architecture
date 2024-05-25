
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  static void AreEqual<T>(
    IEnumerable<T> expected,
    IEnumerable<T>? actual)
  =>
    CollectionAssert.AreEqual(expected.ToArray(), actual?.ToArray());
}