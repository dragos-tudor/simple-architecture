
using Docker.DotNet.Models;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<IEnumerable<ContainerInspectResponse>> InitializeMongeReplicaSetAsync (MongoReplicaSetOptions options, CancellationToken cancellationToken = default)
  {
    var (imageName, containerNames, networkName, replicaSet, dbName, collNames, serverPort) = options;
    using var dockerClient = CreateDockerClient();

    var containers = await StartMongoReplicaSetAsync(dockerClient, imageName, containerNames, serverPort, networkName, replicaSet, cancellationToken);
    var replicaSetNetworkAddresses = JoinReplicaSetNetworkAddresses(containerNames, serverPort);

    var connectionString = GetMongoConnectionString(replicaSetNetworkAddresses, replicaSet);
    var mongoClient = CreateMongoClient(connectionString);
    var mongoDb = GetMongoDatabase(mongoClient, dbName);

    if(!ExistsMongoCollections(mongoDb))
      CreateMongoCollections(mongoDb, collNames);
    MapModelClassTypes();

    return containers;
  }
}