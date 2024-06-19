
using static Docker.Extensions.DockerFuncs;

using System.Threading;

namespace Simple.Web.Testing;

partial class TestingFuncs
{
  static async Task WaitForNotification (string to, TimeSpan? retryAfter = default)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));
    var cancellationToken = cancellationTokenSource.Token;
    var defaultRetryAfter = TimeSpan.FromMilliseconds(50);

    await WaitUntilAsync(
      () => Task.FromResult(NotificationStore.Any(notification => notification.To == to)),
      retryAfter ?? defaultRetryAfter,
      cancellationToken);
  }
}