
namespace Simple.Worker;

partial class WorkerFuncs
{
  public static async Task Main(string[] args)
  {
    using var cancellationTokenSource = new CancellationTokenSource(Timeout.Infinite);
    var hostServer = InitializeHostServer(args, "settings.json", (_) => { }, cancellationTokenSource.Token);

    await hostServer.RunAsync();
    cancellationTokenSource.Cancel();
  }
}