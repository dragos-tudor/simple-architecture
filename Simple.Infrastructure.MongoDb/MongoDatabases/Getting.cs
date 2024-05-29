
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static string GetMongoConnectionString (string ipAddress, int serverPort) => $"mongodb://{ipAddress}:{serverPort}";

  public static IMongoDatabase GetMongoDatabase (string dbName, IMongoClient? mongoClient = default) => (mongoClient ?? MongoDbClient).GetDatabase(dbName);
}