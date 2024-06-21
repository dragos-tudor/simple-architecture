
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static IMongoDatabase GetMongoDatabase (IMongoClient mongoClient, string dbName) => mongoClient.GetDatabase(dbName);

  public static IMongoDatabase GetMongoDatabase (MongoReplicaSetOptions replicaSetOptions) =>
    GetMongoDatabase(CreateMongoClient(GetMongoConnectionString(string.Join(",", replicaSetOptions.ContainerNames), replicaSetOptions.ReplicaSet)), replicaSetOptions.DbName);
}