#pragma warning disable CA2000

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace Simple.Web.Api;

partial class ApiFuncs
{
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
