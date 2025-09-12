
namespace Simple.Worker;

partial class WorkerFuncs
{
  internal static TOptions GetConfigurationOptions<TOptions>(IConfiguration configuration) where TOptions : new() => configuration.GetSection(typeof(TOptions).Name).Get<TOptions>() ?? new();
}