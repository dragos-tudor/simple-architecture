#pragma warning disable CA2000

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static SendNotifications<TNotification> CreateNotificationsSender<TNotification> (
    EmailServerOptions emailServerOptions,
    Func<TNotification, MimeMessage> mapNotification)
  {
    var smtpClient = CreateSmtpClient();
    return (notifications, cancellationToken) => SendNotificationsAsync(smtpClient, notifications, emailServerOptions, mapNotification, cancellationToken);
  }
}