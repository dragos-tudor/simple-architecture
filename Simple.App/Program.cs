#pragma warning disable CA2000

namespace Simple.App;

partial class AppFuncs
{
  public static async Task Main(string[] args)
  {
    var cancellationTokenSource = new CancellationTokenSource(Timeout.Infinite);
    var cancellationToken = cancellationTokenSource.Token;

    var configuration = BuildConfiguration("settings.json");
    var host = BuildHost(args, configuration, (_) => {});
    await IntegrateServersAsync(host, configuration, cancellationToken);

    var hostLifetime = GetRequiredService<IHostApplicationLifetime>(host.Services);
    hostLifetime.ApplicationStopping.Register(cancellationTokenSource.Cancel);
    await host.RunAsync();
  }
}
