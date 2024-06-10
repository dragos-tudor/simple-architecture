#pragma warning disable CA2000

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog.Extensions.Logging;

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
    var app = await StartupAppAsync(configuration, notificationsStore.Add);
    await app.RunAsync();
  }

  public static async Task<WebApplication> StartupAppAsync (IConfiguration configuration, Action<Notification> handleNotification)
  {
    var builder = WebApplication.CreateBuilder();
    builder.Services.AddProblemDetails();
    builder.Configuration.AddConfiguration(configuration);
    var loggerFactory = new SerilogLoggerFactory(IntegrateSerilog(builder, configuration));

    var app = builder.Build();
    app.UseExceptionHandler().UseRouting();

    var appCancellationTokenSource = new CancellationTokenSource(Timeout.Infinite);
    var appCancellationToken = appCancellationTokenSource.Token;

    var (sendNotification, shutdownNotificationServer) = IntegrateNotificationServer(app, handleNotification, loggerFactory);
    await Task.WhenAll([
      IntegrateSqlServerAsync(app, sendNotification, loggerFactory, appCancellationToken),
      IntegrateMongoServerAsync(app, sendNotification, loggerFactory, appCancellationToken)
    ]);

    app.Lifetime.ApplicationStopping.Register(appCancellationTokenSource.Cancel);
    app.Lifetime.ApplicationStopping.Register(() => shutdownNotificationServer());
    return app;
  }
}
