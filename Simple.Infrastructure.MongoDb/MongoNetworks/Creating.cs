
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static Task<NetworksCreateResponse> CreateNetworkAsync (INetworkOperations networks, string networkName, CancellationToken cancellationToken = default) =>
    networks.CreateNetworkAsync(new NetworksCreateParameters() { Name = networkName }, cancellationToken);
}