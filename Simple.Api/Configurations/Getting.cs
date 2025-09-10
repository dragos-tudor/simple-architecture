
using Microsoft.Extensions.Configuration;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  public static TOptions GetConfigurationOptions<TOptions>(IConfiguration configuration) where TOptions : new() => configuration.GetSection(typeof(TOptions).Name).Get<TOptions>() ?? new();
}