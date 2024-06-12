
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static IConfiguration BuildConfiguration (string configPath) => new ConfigurationBuilder().AddJsonFile(configPath).Build();

  internal static IConfigurationBuilder LoadConfiguration (WebApplicationBuilder builder, IConfiguration configuration) => builder.Configuration.AddConfiguration(configuration);
}