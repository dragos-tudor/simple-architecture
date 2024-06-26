
namespace Simple.App;

partial class AppFuncs
{
  internal static IConfiguration BuildConfiguration (string configPath) => new ConfigurationBuilder().AddJsonFile(configPath).Build();
}