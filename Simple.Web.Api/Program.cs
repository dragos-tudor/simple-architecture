
using System.Collections.Concurrent;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Simple.Web.Api;

public static class Program
{
  public static async Task Main (string[] _)
  {
    var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json")
      .Build();

    var notificationsStore = new ConcurrentBag<Notification>();
    using var loggerFactory = IntegrateSerilog(configuration);

    var app = await StartupAppAsync(loggerFactory, notificationsStore.Add);
    await app.RunAsync();
  }
}
