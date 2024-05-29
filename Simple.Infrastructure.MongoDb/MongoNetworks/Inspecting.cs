
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static Task<NetworkResponse> InspectNetworkAsync (INetworkOperations networks, string networkName, CancellationToken cancellationToken = default) =>
    networks.InspectNetworkAsync(networkName, cancellationToken);
}