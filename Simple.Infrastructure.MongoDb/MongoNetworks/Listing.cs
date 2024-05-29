
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static Task<IList<NetworkResponse>> ListNetworksAsync (INetworkOperations networks, CancellationToken cancellationToken = default) =>
    networks.ListNetworksAsync(default, cancellationToken);
}