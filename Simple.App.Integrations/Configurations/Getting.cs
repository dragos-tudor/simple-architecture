
using Microsoft.Extensions.Configuration;

namespace Simple.App.Integrations;

partial class IntegrationFuncs
{
  public static TOptions GetConfigurationOptions<TOptions> (IConfiguration configuration) => configuration.GetSection(typeof(TOptions).Name).Get<TOptions>()!;
}