
namespace Simple.Worker;

partial class WorkerFuncs
{
  internal static TOptions GetConfigurationOptions<TOptions>(IConfiguration configuration, string? configName = default) where TOptions : new() =>
    configuration.GetSection(configName ?? typeof(TOptions).Name).Get<TOptions>() ?? new();
}