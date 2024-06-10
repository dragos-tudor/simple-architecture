#pragma warning disable CA2000

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.IO;
using Microsoft.Extensions.Configuration;

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

  public static async Task<WebApplication> StartupAppAsync (ILoggerFactory loggerFactory, Action<Notification> handleNotification)
  {
    var builder = WebApplication.CreateBuilder();
    builder.Services.AddProblemDetails();

    var app = builder.Build();
    app.UseExceptionHandler().UseRouting();

    var appCancellationTokenSource = new CancellationTokenSource(Timeout.Infinite);
    var appCancellationToken = appCancellationTokenSource.Token;

    var (sendNotification, shutdownNotificationServer) = IntegrateNotificationServer(app, handleNotification, loggerFactory);
    await Task.WhenAll([
      IntegrateSqlServerAsync(app, sendNotification, loggerFactory, appCancellationToken),
      IntegrateMongoServerAsync(app, sendNotification, loggerFactory, appCancellationToken)
    ]);

    var appLogger = loggerFactory.CreateLogger(typeof(ApiFuncs).Namespace!);
    app.Lifetime.ApplicationStopping.Register(() => LogShutingDownApp(appLogger));

    app.Lifetime.ApplicationStopping.Register(appCancellationTokenSource.Cancel);
    app.Lifetime.ApplicationStopping.Register(() => shutdownNotificationServer());
    return app;
  }
}
