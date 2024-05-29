
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  internal static bool ExistsMongoCollections (IMongoDatabase database) => database.ListCollectionNames().Any();
}