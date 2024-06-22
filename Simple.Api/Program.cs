#pragma warning disable CA2000

namespace Simple.Api;

public sealed partial class ApiFuncs
{
  public static async Task Main (string[] args)
  {
    var cancellationTokenSource = new CancellationTokenSource(Timeout.Infinite);
    var cancellationToken = cancellationTokenSource.Token;
    var configuration = BuildConfiguration("settings.json");
    var app = BuildApplication(args, configuration, (_) => {});

    var loggerFactory = GetRequiredService<ILoggerFactory>(app.Services);
    var serverIntegrations = await IntegrateServersAsync(configuration, RegisterMongoSubscribers, RegisterSqlSubscribers, loggerFactory, cancellationToken);
    IntegrateApi(app, serverIntegrations, loggerFactory);

    app.Lifetime.ApplicationStopping.Register(cancellationTokenSource.Cancel);
    await app.RunAsync();
  }
}