#pragma warning disable CA2000

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Simple.Infrastructure.Notifications;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static async Task Main (string[] _)
  {
    var configuration = BuildConfiguration("settings.json");
    var notificationStore = new ConcurrentBag<Notification>();
    var (app, _, _, _) = await StartupAppAsync(configuration, (_) => {}, notificationStore.Add);
    await app.RunAsync();
  }

  public static async Task<(WebApplication, AgendaContextFactory, IMongoDatabase, ReceiveNotifications<Notification>)> StartupAppAsync (IConfiguration configuration, Action<WebApplicationBuilder> configBuilder, Action<Notification> handleNotification)
  {
    var builder = WebApplication.CreateBuilder();
    RegisterLogging(builder, IntegrateSerilog(configuration));
    RegisterServices(builder);
    configBuilder(builder);

    var app = builder.Build();
    UseMiddlewares(app);

    var appCancellationTokenSource = new CancellationTokenSource(Timeout.Infinite);
    var appCancellationToken = appCancellationTokenSource.Token;
    var loggerFactory = GetRequiredService<ILoggerFactory>(app);

    var (sendNotifications, receiveNotifications) = await IntegrateNotificationServerAsync(configuration, appCancellationToken);
    var mongoTask = IntegrateMongoReplicaSetAsync(configuration, (notification, cancellationToken) => sendNotifications([notification], cancellationToken), loggerFactory, appCancellationToken);
    var sqlServerTask = IntegrateSqlServerAsync(configuration, (notification, cancellationToken) => sendNotifications([notification], cancellationToken), loggerFactory, appCancellationToken);
    await Task.WhenAll([mongoTask, sqlServerTask]);

    var (agendaDb, mongoMessageQueue) = await mongoTask;
    var (agendaContextFactory, sqlMessageQueue) = await sqlServerTask;
    var domainLogger = loggerFactory.CreateLogger(typeof(ServicesFuncs).Namespace!);

    MapMongoEndpoints(app, agendaDb, mongoMessageQueue, domainLogger);
    MapSqlEndpoints(app, agendaContextFactory, sqlMessageQueue, domainLogger);

    app.Lifetime.ApplicationStopping.Register(appCancellationTokenSource.Cancel);
    return (app, agendaContextFactory, agendaDb, receiveNotifications);
  }
}
