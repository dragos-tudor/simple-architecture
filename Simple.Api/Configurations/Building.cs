
namespace Simple.Api;

partial class ApiFuncs
{
 public static IConfiguration BuildConfiguration (string configPath) => new ConfigurationBuilder().AddJsonFile(configPath).Build();
}