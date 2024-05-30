
using Docker.DotNet;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static async Task<string> StartSqlServerAsync (
    IDockerClient client, string adminPassword,
    string imageName, string containerName,
    int serverPort, string networkName,
    CancellationToken cancellationToken = default)
  {
    await UseNetworkAsync(client.Networks, networkName, cancellationToken);
    var container = await UseContainerAsync(client, imageName, containerName, SetSqlCreateContainerParameters(adminPassword, containerName, networkName), cancellationToken);

    await WaitForOpenPortWtihBash(client.Exec, container.ID, serverPort, cancellationToken);
    var endpointSettings = GetNetworkEndpoints(container.NetworkSettings, networkName);
    return GetEdpointIpAddress(endpointSettings);
  }
}