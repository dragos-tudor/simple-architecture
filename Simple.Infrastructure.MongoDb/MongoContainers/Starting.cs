
using Docker.DotNet.Models;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<ContainerInspectResponse> StartMongoContainerAsync (
    int serverPort,
    string imageName,
    string containerName,
    string networkName,
    string replicaSet,
    CancellationToken cancellationToken = default)
  {
    using var client = CreateDockerClient();
    await CreateDockerImageAsync(client.Images, imageName, cancellationToken);

    var createContainerParameters = SetMongoCreateContainerParameters(serverPort, containerName, networkName, replicaSet);
    var containerId = await UseContainerAsync(client.Containers, imageName, containerName, createContainerParameters, cancellationToken);
    await WaitForOpenPort(client.Exec, containerId, serverPort, cancellationToken);

    return (await InspectContainerAsync(client.Containers, containerId, cancellationToken))!;
  }

  public static async Task<IEnumerable<ContainerInspectResponse>> StartMongoContainers (
    int serverPort,
    string imageName,
    string[] containerNames,
    string networkName,
    string replicaSet,
    CancellationToken cancellationToken = default)
  =>
    await Task.WhenAll(containerNames
      .Select(async containerName => await StartMongoContainerAsync(serverPort, imageName, containerName, networkName, replicaSet, cancellationToken))
      .ToList());
}