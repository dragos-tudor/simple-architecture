
using Simple.Infrastructure.Integrations;

namespace Simple.App;

partial class AppFuncs
{
  static async Task<IHost> IntegrateServersAsync (IHost host, IConfiguration configuration, CancellationToken cancellationToken = default)
  {
    var loggerFactory = GetRequiredService<ILoggerFactory>(host.Services);

    await IntegrationsFuncs.IntegrateServersAsync(configuration, RegisterMongoSubscribers,  RegisterSqlSubscribers, loggerFactory, cancellationToken);

    return host;
  }
}
