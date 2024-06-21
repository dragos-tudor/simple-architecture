
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  public static async Task<ContainerInspectResponse> StartEmailServerAsync (
    IDockerClient client,
    EmailServerOptions serverOptions,
    CancellationToken cancellationToken = default)
  {
    var (imageName, containerName, networkName, imapPort, pop3Port, smtpPort) = serverOptions;

    await UseNetworkAsync(client.Networks, networkName, cancellationToken);
    var createContainerParameters = SetEmailCreateContainerParameters(containerName, imapPort, pop3Port, smtpPort, networkName);
    var container = await UseContainerAsync(client, imageName, containerName, createContainerParameters, cancellationToken);

    await WaitForOpenPortWtihBash(client.Exec, container.ID, imapPort, cancellationToken);
    await WaitForOpenPortWtihBash(client.Exec, container.ID, pop3Port, cancellationToken);
    await WaitForOpenPortWtihBash(client.Exec, container.ID, smtpPort, cancellationToken);
    return container;
  }
}