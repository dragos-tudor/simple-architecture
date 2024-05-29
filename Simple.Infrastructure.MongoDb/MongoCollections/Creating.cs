
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static void CreateMongoCollection (IMongoDatabase database, string collection) => database.CreateCollection(collection);

  public static void CreateMongoCollections (IMongoDatabase database, params string[] collections)
  {
    foreach (var collection in collections)
      CreateMongoCollection(database, collection);
  }
}