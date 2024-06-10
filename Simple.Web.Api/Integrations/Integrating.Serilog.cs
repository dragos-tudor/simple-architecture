#pragma warning disable CA2000

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Extensions.Logging;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static Serilog.ILogger IntegrateSerilog (WebApplicationBuilder builder, IConfiguration configuration)
  {
    var loggerConfiguration = new LoggerConfiguration();
    var logger = loggerConfiguration.ReadFrom.Configuration(configuration).CreateLogger();

    builder.Logging.ClearProviders();
    builder.Logging.AddProvider(new SerilogLoggerProvider(logger, false));
    return logger;
  }
}