
using Docker.DotNet.Models;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<IEnumerable<ContainerInspectResponse>> InitializeMongeReplicaSetAsync (MongoReplicaSetOptions options, CancellationToken cancellationToken = default)
  {
    var (imageName, containerNames, networkName, replicaSet, serverPort, collNames) = options;
    using var dockerClient = CreateDockerClient();

    var containers = await StartMongoReplicaSetAsync(dockerClient, imageName, containerNames, serverPort, networkName, replicaSet, cancellationToken);
    var replicaSetNetworkAddresses = JoinReplicaSetNetworkAddresses(containerNames, serverPort);

    var connectionString = GetMongoConnectionString(replicaSetNetworkAddresses, replicaSet);
    var mongoClient = CreateMongoClient(connectionString);
    var database = GetMongoDatabase(mongoClient, DatabaseName);

    if(!ExistsMongoCollections(database))
      CreateMongoCollections(database, collNames);
    MapModelClassTypes();

    MongoDbClient = mongoClient;
    return containers;
  }
}