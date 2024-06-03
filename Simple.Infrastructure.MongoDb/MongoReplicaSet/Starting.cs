
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<IEnumerable<ContainerInspectResponse>> StartMongoReplicaSetAsync (
    IDockerClient client,
    string imageName, string[] containerNames,
    int serverPort, string networkName,
    string replicaSet, CancellationToken cancellationToken = default)
  {
    await UseNetworkAsync(client.Networks, networkName, cancellationToken);
    var containers = await Task.WhenAll(UseMongoServersAsync(client, imageName, containerNames, serverPort, networkName, replicaSet, cancellationToken).ToList());
    var containerId = containers.First().ID;

    await WaitForMongoReplicaSet(client.Exec, containerId, serverPort, replicaSet, containerNames, cancellationToken);
    return containers;
  }
}