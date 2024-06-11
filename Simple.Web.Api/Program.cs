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
      .AddJsonFile("settings.json")
      .Build();

    var notificationsStore = new ConcurrentBag<Notification>();
    var configBuilder = (WebApplicationBuilder builder) => IntegrateSerilog(builder, configuration);
    var app = await StartupAppAsync(configuration, configBuilder, notificationsStore.Add);
    await app.RunAsync();
  }

  public static async Task<WebApplication> StartupAppAsync (IConfiguration configuration, Action<WebApplicationBuilder> configBuilder, Action<Notification> handleNotification)
  {
    var builder = WebApplication.CreateBuilder();
    configBuilder(builder);
    builder.Services.AddProblemDetails();
    builder.Configuration.AddConfiguration(configuration);

    var app = builder.Build();
    app.UseExceptionHandler().UseRouting();

    var appCancellationTokenSource = new CancellationTokenSource(Timeout.Infinite);
    var appCancellationToken = appCancellationTokenSource.Token;
    var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

    var (sendNotification, shutdownNotificationServer) = IntegrateNotificationServer(app, handleNotification, loggerFactory);
    await Task.WhenAll([
      IntegrateSqlServerAsync(app, sendNotification, loggerFactory, appCancellationToken),
      IntegrateMongoReplicaSetAsync(app, sendNotification, loggerFactory, appCancellationToken)
    ]);

    app.Lifetime.ApplicationStopping.Register(appCancellationTokenSource.Cancel);
    app.Lifetime.ApplicationStopping.Register(() => shutdownNotificationServer());
    return app;
  }
}
