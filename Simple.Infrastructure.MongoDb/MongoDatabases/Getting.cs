
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static IMongoDatabase GetMongoDatabase(IMongoClient mongoClient, string dbName) => mongoClient.GetDatabase(dbName);

  public static IMongoDatabase GetMongoDatabase(MongoOptions options) =>
    GetMongoDatabase(options.ServerName, options.ServerPort, options.DbName);

  public static IMongoDatabase GetMongoDatabase(string serverName, int serverPort, string dbName) =>
    GetMongoDatabase(CreateMongoClient(GetMongoConnectionString(serverName, serverPort)), dbName);
}