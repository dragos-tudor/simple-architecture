
namespace Simple.Api;

partial class ApiFuncs
{
  internal static async Task<ServerIntegrations> IntegrateServersAndApiAsync (WebApplication app, IConfiguration configuration, CancellationToken cancellationToken = default)
  {
    var loggerFactory = GetRequiredService<ILoggerFactory>(app.Services);

    var serverIntegrations = await IntegrateServersAsync(configuration, loggerFactory, cancellationToken);
    IntegrateApi(app, serverIntegrations, loggerFactory);

    return serverIntegrations;
  }
}
