
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static async Task<string> InitializeEmailServerAsync (EmailServerOptions emailServerOptions, CancellationToken cancellationToken = default)
  {
    using var dockerClient = CreateDockerClient();
    var container = await StartEmailServerAsync (dockerClient, emailServerOptions, cancellationToken);
    return container.Name;
  }
}
