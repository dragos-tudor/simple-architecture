
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static IConfiguration BuildConfiguration (string configPath) => new ConfigurationBuilder().AddJsonFile(configPath).Build();

  static TOptions GetConfigurationOptions<TOptions> (WebApplication app) => app.Configuration.GetSection(typeof(TOptions).Name).Get<TOptions>()!;

  static IConfigurationBuilder LoadConfiguration (WebApplicationBuilder builder, IConfiguration configuration) => builder.Configuration.AddConfiguration(configuration);
}