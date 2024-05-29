
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static void CleanMongoCollections (IMongoDatabase database, params string[] collections) => CleanupCollections(database, collections);
}