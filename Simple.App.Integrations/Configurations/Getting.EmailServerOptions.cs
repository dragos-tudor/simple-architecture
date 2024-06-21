
using Microsoft.Extensions.Configuration;

namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  public static EmailServerOptions GetEmailServerOptions (IConfiguration configuration) => GetConfigurationOptions<EmailServerOptions>(configuration);
}