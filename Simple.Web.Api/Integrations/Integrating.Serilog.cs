
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Extensions.Logging;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static ILoggerFactory IntegrateSerilog (IConfiguration configuration) =>
    new SerilogLoggerFactory(new LoggerConfiguration()
      .ReadFrom.Configuration(configuration)
      .CreateLogger());
}