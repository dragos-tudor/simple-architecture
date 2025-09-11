
using Serilog;
using Serilog.Extensions.Logging;

namespace Simple.Api;

partial class ApiFuncs
{
  static ILoggerFactory CreateSerilogFactory(IConfiguration configuration)
  {
    var loggerConfiguration = new LoggerConfiguration();
    var logger = loggerConfiguration.ReadFrom.Configuration(configuration).CreateLogger();

    using var services = new LoggerProviderCollection();
    services.AddProvider(new SerilogLoggerProvider(logger, false));
    return new SerilogLoggerFactory(logger, providerCollection: services);
  }
}