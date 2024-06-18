#pragma warning disable CA2000

using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Extensions.Logging;

namespace Simple.App.Integrations;

partial class IntegrationFuncs
{
  public static ILoggerFactory IntegrateSerilog (IConfiguration configuration)
  {
    var loggerConfiguration = new LoggerConfiguration();
    var logger = loggerConfiguration.ReadFrom.Configuration(configuration).CreateLogger();

    using var providerCollection = new LoggerProviderCollection();
    providerCollection.AddProvider(new SerilogLoggerProvider(logger, false));
    return new SerilogLoggerFactory(logger, providerCollection: providerCollection);
  }
}