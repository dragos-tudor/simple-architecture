
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static async Task<ContainerInspectResponse> StartEmailServerAsync (
    IDockerClient client,
    EmailServerOptions emailServerOptions,
    CancellationToken cancellationToken = default)
  {
    var (imageName, containerName, networkName, imapPort, pop3Port, smtpPort) = emailServerOptions;

    await UseNetworkAsync(client.Networks, networkName, cancellationToken);
    var createContainerParameters = SetEmailCreateContainerParameters(containerName, imapPort, pop3Port, smtpPort, networkName);
    var container = await UseContainerAsync(client, imageName, containerName, createContainerParameters, cancellationToken);

    await WaitForOpenPortWtihBash(client.Exec, container.ID, imapPort, cancellationToken);
    await WaitForOpenPortWtihBash(client.Exec, container.ID, pop3Port, cancellationToken);
    await WaitForOpenPortWtihBash(client.Exec, container.ID, smtpPort, cancellationToken);
    return container;
  }
}