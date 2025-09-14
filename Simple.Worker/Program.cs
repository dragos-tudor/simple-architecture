
namespace Simple.Worker;

partial class WorkerFuncs
{
  public static async Task Main(string[] args)
  {
    using var cancellationTokenSource = new CancellationTokenSource(Timeout.Infinite);
    var worketServer = InitializeWorkerServer(args, "settings.json", (_) => { }, cancellationTokenSource.Token);

    await worketServer.RunAsync();
    cancellationTokenSource.Cancel();
  }
}