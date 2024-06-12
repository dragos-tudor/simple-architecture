#pragma warning disable CA2000

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Simple.Web.Api;

public static class Program
{
  public static async Task Main (string[] _)
  {
    var configuration = BuildConfiguration("settings.json");
    var notificationStore = new ConcurrentBag<Notification>();
    var (app, _, _) = await StartupAppAsync(configuration, (_) => {}, notificationStore.Add);
    await app.RunAsync();
  }

  public static async Task<(WebApplication, AgendaContextFactory, IMongoDatabase)> StartupAppAsync (IConfiguration configuration, Action<WebApplicationBuilder> configBuilder, Action<Notification> handleNotification)
  {
    var builder = WebApplication.CreateBuilder();
    LoadConfiguration(builder, configuration);
    IntegrateSerilog(builder, configuration);
    RegisterServices(builder);
    configBuilder(builder);

    var app = builder.Build();
    UseMiddlewares(app);

    var appCancellationTokenSource = new CancellationTokenSource(Timeout.Infinite);
    var appCancellationToken = appCancellationTokenSource.Token;
    var loggerFactory = GetRequiredService<ILoggerFactory>(app);

    var (sendNotification, shutdownNotificationServer) = IntegrateNotificationServer(app, handleNotification, loggerFactory);
    var agendaContextFactoryTask = IntegrateSqlServerAsync(app, sendNotification, loggerFactory, appCancellationToken);
    var mongoDbTask = IntegrateMongoReplicaSetAsync(app, sendNotification, loggerFactory, appCancellationToken);
    await Task.WhenAll([agendaContextFactoryTask, mongoDbTask]);

    app.Lifetime.ApplicationStopping.Register(appCancellationTokenSource.Cancel);
    app.Lifetime.ApplicationStopping.Register(() => shutdownNotificationServer());
    return (app, await agendaContextFactoryTask, await mongoDbTask);
  }
}
