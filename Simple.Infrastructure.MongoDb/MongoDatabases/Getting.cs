
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static IMongoDatabase GetMongoDatabase (IMongoClient mongoClient, string dbName) => mongoClient.GetDatabase(dbName);
}