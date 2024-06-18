
using Microsoft.Extensions.Configuration;

namespace Simple.App.Integrations;

partial class IntegrationFuncs
{
  public static (SendNotification<Notification>, ShutdownServer) IntegrateNotificationServer (IConfiguration configuration, Action<Notification> handleNotification, ILoggerFactory loggerFactory)
  {
    var notificationServerOptions = GetConfigurationOptions<NotificationServerOptions>(configuration);
    var notificationsLogger = loggerFactory.CreateLogger(typeof(NotificationsFuncs).Namespace!);

    var shutdownNotificationServer = StartNotificationServer(
      notificationServerOptions,
      (notification) => {
        handleNotification(notification);
        LogSentNotification(notificationsLogger, notification.From, notification.To, notification.Title);
      },
      MapMessage<Notification>);
    var sendNotification = CreateNotificationSender<Notification>(notificationServerOptions, MapNotification);

    return ((notification, cancellationToken) => sendNotification(notification, cancellationToken), shutdownNotificationServer);
  }
}
