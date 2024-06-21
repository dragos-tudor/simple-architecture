
namespace Simple.App;

partial class AppFuncs
{
  static async Task<IHost> IntegrateServersAsync (IHost host, IConfiguration configuration, CancellationToken cancellationToken = default)
  {
    var loggerFactory = GetRequiredService<ILoggerFactory>(host.Services);

    await Integrations.IntegrationsFuncs.IntegrateServersAsync(configuration, loggerFactory, cancellationToken);

    return host;
  }
}
