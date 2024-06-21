
namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  public static async Task<string> InitializeEmailServerAsync (EmailServerOptions serverOptions, CancellationToken cancellationToken = default)
  {
    using var dockerClient = CreateDockerClient();
    var container = await StartEmailServerAsync (dockerClient, serverOptions, cancellationToken);
    return container.Name;
  }
}
