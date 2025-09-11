
namespace Simple.Api;

partial class ApiFuncs
{
  static string? GetConfiguration(IConfiguration configuration, string name) => configuration[name];

  static TOptions GetConfigurationOptions<TOptions>(IConfiguration configuration) where TOptions : new() => configuration.GetSection(typeof(TOptions).Name).Get<TOptions>() ?? new();
}