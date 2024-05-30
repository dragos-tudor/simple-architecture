
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  static IEnumerable<Task<ContainerInspectResponse>> UseMongoServersAsync (
    IDockerClient client,
    string imageName, string[] containerNames,
    int serverPort, string networkName,
    string replicaSet, CancellationToken cancellationToken)
  {
    foreach(var containerName in containerNames)
      yield return UseContainerAsync(
        client, imageName, containerName,
        SetMongoCreateContainerParameters(serverPort, containerName, networkName, replicaSet),
        cancellationToken);
  }
}