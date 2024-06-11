#pragma warning disable CA2000

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Extensions.Logging;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static void IntegrateSerilog (WebApplicationBuilder builder, IConfiguration configuration)
  {
    var loggerConfiguration = new LoggerConfiguration();
    var logger = loggerConfiguration.ReadFrom.Configuration(configuration).CreateLogger();

    builder.Logging.ClearProviders();
    builder.Logging.AddProvider(new SerilogLoggerProvider(logger, false));
    builder.Services.AddSingleton(new SerilogLoggerFactory(logger));
  }
}