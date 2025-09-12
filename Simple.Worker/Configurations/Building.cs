
namespace Simple.Worker;

partial class WorkerFuncs
{
  internal static IConfiguration BuildConfiguration(string configPath) => new ConfigurationBuilder().AddJsonFile(configPath).Build();
}