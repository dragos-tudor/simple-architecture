
namespace Simple.App;

partial class AppFuncs
{
 public static IConfiguration BuildConfiguration (string configPath) => new ConfigurationBuilder().AddJsonFile(configPath).Build();
}