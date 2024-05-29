
using Docker.DotNet.Models;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  static Action<CreateContainerParameters> SetMongoCreateContainerParameters (int serverPort, string containerName, string networkName, string replicaSet) => (CreateContainerParameters @params) =>
  {
    @params.Hostname = containerName;
    @params.HostConfig = new HostConfig() { NetworkMode = networkName };
    @params.Cmd = [$"--replSet={replicaSet}", "--bind_ip_all", $"--port={serverPort}"];
  };
}