
namespace Simple.Api;

partial class ApiFuncs
{
  internal static IConfiguration BuildConfiguration(string configPath) => new ConfigurationBuilder().AddJsonFile(configPath).Build();
}