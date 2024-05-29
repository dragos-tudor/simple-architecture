
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<MongoClient> StartMongoServer (string imageName,
    string[] containerNames,
    int serverPort,
    string networkName,
    string replicaSet,
    CancellationToken cancellationToken = default)
  {
    await UseNetworkAsync(networkName, cancellationToken);
    var containers = await StartMongoContainers(serverPort, imageName, containerNames, networkName, replicaSet, cancellationToken);
    await InitializeMongoReplicaSetAsync(serverPort, GetMasterContainerId(containers), containerNames, replicaSet, cancellationToken);

    var endpointSettings = GetEndpointSettings(containers.First().NetworkSettings, networkName);
    var serverIpAddress = GetServerIpAddress(endpointSettings);
    var connectionString = GetMongoConnectionString(serverIpAddress, serverPort);

    return CreateMongoClient(connectionString);
  }
}