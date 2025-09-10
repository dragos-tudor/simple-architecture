
using Microsoft.Extensions.Configuration;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  public static IConfiguration BuildConfiguration (string configPath) => new ConfigurationBuilder().AddJsonFile(configPath).Build();
}