
using Microsoft.Extensions.Configuration;

namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  public static TOptions GetConfigurationOptions<TOptions> (IConfiguration configuration) where TOptions: new() => configuration.GetSection(typeof(TOptions).Name).Get<TOptions>() ?? new();
}