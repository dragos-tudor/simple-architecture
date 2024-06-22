
using Microsoft.Extensions.Configuration;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  static TOptions GetConfigurationOptions<TOptions> (IConfiguration configuration) where TOptions: new() => configuration.GetSection(typeof(TOptions).Name).Get<TOptions>() ?? new();

  public static EmailServerOptions GetEmailServerOptions (IConfiguration configuration) => GetConfigurationOptions<EmailServerOptions>(configuration);
}