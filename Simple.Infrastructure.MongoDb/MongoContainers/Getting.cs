using Docker.DotNet.Models;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  static string GetMasterContainerId (IEnumerable<ContainerInspectResponse> containers) => containers.First().ID;

}