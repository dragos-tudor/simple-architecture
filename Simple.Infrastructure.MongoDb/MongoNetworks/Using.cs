
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<string> UseNetworkAsync (string networkName, CancellationToken cancellationToken = default)
  {
    using var client = CreateDockerClient();
    var networks = client.Networks;

    var networkResponses = await ListNetworksAsync(networks, cancellationToken);
    var networkResponse = GetNamedNetwork(networkResponses, networkName);
    if (ExistNetwork(networkResponse)) return networkResponse!.ID;

    var networkCreateResponse = await CreateNetworkAsync(networks, networkName, cancellationToken);
    return networkCreateResponse.ID;
  }
}