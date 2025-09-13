
namespace Simple.Testing.Models;

partial class ModelsFuncs
{
  public static async Task WaitWhileAsync(Func<Task<bool>> wait, TimeSpan retryAfter, TimeSpan timeout)
  {
    using var cancellationTokenSource = new CancellationTokenSource(timeout);
    var cancellationToken = cancellationTokenSource.Token;

    while (!cancellationToken.IsCancellationRequested)
    {
      var isSuccessful = await wait.Invoke();
      if (!isSuccessful) break;

      await Task.Delay(retryAfter, cancellationToken);
    }
  }

  public static async Task WaitUntilAsync(Func<Task<bool>> wait, TimeSpan retryAfter, TimeSpan timeout)
  {
    using var cancellationTokenSource = new CancellationTokenSource(timeout);
    var cancellationToken = cancellationTokenSource.Token;

    while (!cancellationToken.IsCancellationRequested)
    {
      var isSuccessful = await wait.Invoke();
      if (isSuccessful) break;

      await Task.Delay(retryAfter, cancellationToken);
    }
  }
}

