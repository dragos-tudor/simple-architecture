
namespace Simple.App;

partial class AppFuncs
{
  static TOptions GetConfigurationOptions<TOptions> (IConfiguration configuration) where TOptions: new() => configuration.GetSection(typeof(TOptions).Name).Get<TOptions>() ?? new();
}