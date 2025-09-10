
using Microsoft.Extensions.Configuration;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  public static IConfigurationBuilder AddConfiguration (ConfigurationManager manager, IConfiguration configuration) => manager.AddConfiguration(configuration);
}