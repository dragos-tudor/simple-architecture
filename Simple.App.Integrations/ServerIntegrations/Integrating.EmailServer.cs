
namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  public static Task IntegrateEmailServerAsync (EmailServerOptions serverOptions, CancellationToken cancellationToken = default) =>
    InitializeEmailServerAsync (serverOptions, cancellationToken);
}
