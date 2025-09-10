
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static IMongoDatabase GetMongoDatabase(IMongoClient mongoClient, string dbName) => mongoClient.GetDatabase(dbName);

  public static IMongoDatabase GetMongoDatabase(string serverName, int serverPort, string dbName) =>
    GetMongoDatabase(CreateMongoClient(GetMongoConnectionString(serverName, serverPort)), dbName);
}