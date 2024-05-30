
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<string> InitializeMongeReplicaSetAsync (MongoReplicaSetOptions options, CancellationToken cancellationToken = default)
  {
    var (imageName, containerNames, networkName, replicaSet, serverPort, collNames) = options;
    using var dockerClient = CreateDockerClient();
    var networkAddress = await StartMongoReplicaSetAsync(dockerClient, imageName, containerNames, serverPort, networkName, replicaSet, cancellationToken);

    var connectionString = GetMongoConnectionString(networkAddress, serverPort);
    var mongoClient = CreateMongoClient(connectionString);
    var database = GetMongoDatabase(mongoClient, DatabaseName);

    if(!ExistsMongoCollections(database))
      CreateMongoCollections(database, collNames);
    MapModelClassTypes();

    MongoDbClient = mongoClient;
    return connectionString;
  }
}