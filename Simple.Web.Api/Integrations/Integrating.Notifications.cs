
using Microsoft.AspNetCore.Builder;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static (SendNotification<Notification>, ShutdownServer) IntegrateNotificationServer (WebApplication app, Action<Notification> handleNotification, ILoggerFactory loggerFactory)
  {
    var notificationServerOptions = GetConfigurationOptions<NotificationServerOptions>(app);
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
