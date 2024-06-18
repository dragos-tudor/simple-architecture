
using Microsoft.Extensions.Configuration;

namespace Simple.App.Integrations;

partial class IntegrationFuncs
{
 public static IConfiguration BuildConfiguration (string configPath) => new ConfigurationBuilder().AddJsonFile(configPath).Build();
}