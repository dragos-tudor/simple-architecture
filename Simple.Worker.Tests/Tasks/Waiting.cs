
namespace Simple.Worker;

partial class WorkerTests
{
  public static async Task WaitWhileAsync(Func<Task<bool>> wait, TimeSpan retryAfter, CancellationToken cancellationToken = default)
  {
    while (!cancellationToken.IsCancellationRequested)
    {
      var isSuccessful = await wait.Invoke();
      if (!isSuccessful) break;

      await Task.Delay(retryAfter, cancellationToken);
    }
  }

  public static async Task WaitUntilAsync(Func<Task<bool>> wait, TimeSpan retryAfter, CancellationToken cancellationToken = default)
  {
    while (!cancellationToken.IsCancellationRequested)
    {
      var isSuccessful = await wait.Invoke();
      if (isSuccessful) break;

      await Task.Delay(retryAfter, cancellationToken);
    }
  }
}

