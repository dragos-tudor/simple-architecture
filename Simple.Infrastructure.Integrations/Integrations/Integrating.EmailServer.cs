
namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  public static Task IntegrateEmailServerAsync (EmailServerOptions serverOptions, CancellationToken cancellationToken = default) =>
    InitializeEmailServerAsync (serverOptions, cancellationToken);
}
