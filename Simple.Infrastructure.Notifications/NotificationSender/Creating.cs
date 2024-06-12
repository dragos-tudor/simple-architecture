#pragma warning disable CA2000

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static Func<TNotification, CancellationToken, Task> CreateNotificationSender<TNotification> (NotificationServerOptions options, Func<TNotification, MimeMessage> mapNotification)
  {
    var smtpClient = CreateSmtpClient();

    return async (notification, cancellationToken) => {
      using var message = mapNotification(notification);
      await SendMailMessage(smtpClient, message, options.ServerName, options.ServerPort, cancellationToken);
    };
  }
}