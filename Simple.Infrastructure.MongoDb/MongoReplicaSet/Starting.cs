
using Docker.DotNet;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<string> StartMongoReplicaSetAsync (
    IDockerClient client,
    string imageName, string[] containerNames,
    int serverPort, string networkName,
    string replicaSet, CancellationToken cancellationToken = default)
  {
    await UseNetworkAsync(client.Networks, networkName, cancellationToken);
    var containers = await Task.WhenAll(UseMongoServersAsync(client, imageName, containerNames, serverPort, networkName, replicaSet, cancellationToken).ToList());

    await WaitForMongoReplicaSet(client.Exec, GetMasterContainerId(containers), serverPort, replicaSet, containerNames, cancellationToken);
    var endpointSettings = GetNetworkEndpoints(containers.First().NetworkSettings, networkName);
    return GetEdpointIpAddress(endpointSettings);
  }
}