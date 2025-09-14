
namespace Simple.Api;

partial class ApiFuncs
{
  public static async Task Main(string[] args)
  {
    using var cancellationTokenSource = new CancellationTokenSource(Timeout.Infinite);
    var apiServer = InitializeApiServer(args, "settings.json", (_) => { }, cancellationTokenSource.Token);

    await apiServer.RunAsync();
    cancellationTokenSource.Cancel();
  }
}