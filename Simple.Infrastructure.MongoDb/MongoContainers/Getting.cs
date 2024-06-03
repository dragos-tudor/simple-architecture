using Docker.DotNet.Models;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  static string GetFirstContainerId (IEnumerable<ContainerInspectResponse> containers) => containers.First().ID;

}