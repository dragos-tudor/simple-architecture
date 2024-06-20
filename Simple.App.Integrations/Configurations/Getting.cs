
using Microsoft.Extensions.Configuration;

namespace Simple.App.Integrations;

partial class IntegrationFuncs
{
  public static TOptions GetConfigurationOptions<TOptions> (IConfiguration configuration) where TOptions: new() => configuration.GetSection(typeof(TOptions).Name).Get<TOptions>() ?? new();
}