
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
  static void AreEqual<T>(IEnumerable<T> expected, IEnumerable<T>? actual) =>
    CollectionAssert.AreEqual(expected.ToArray(), actual?.ToArray());
}