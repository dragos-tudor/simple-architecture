
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  internal const string DatabaseName = "agenda";

  public static IMongoDatabase GetMongoDatabase (IMongoClient? mongoClient = default, string dbName = DatabaseName) => (mongoClient ?? MongoDbClient).GetDatabase(dbName);
}