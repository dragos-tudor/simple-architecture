
using Microsoft.Extensions.Hosting;

namespace Simple.App.Services;

public class JobSchedulerService : BackgroundService
{
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
      await Task.Delay(1000, stoppingToken);
  }
}
