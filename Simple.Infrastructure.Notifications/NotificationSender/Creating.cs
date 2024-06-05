#pragma warning disable CA2000

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static SendNotification<Notification> CreateNotificationSender (string serverName, int serverPort)
  {
    var smtpClient = CreateSmtpClient();

    return async (notification, cancellationToken) => {
      using var message = MapNotification(notification);
      await SendMailMessage(smtpClient, message, serverName, serverPort, cancellationToken);
    };
  }

  public static Func<TNotification, CancellationToken, Task> CreateNotificationSender<TNotification> (
    string serverName, int serverPort, Func<TNotification, MimeMessage> mapNotification)
  {
    var smtpClient = CreateSmtpClient();

    return async (notification, cancellationToken) => {
      using var message = mapNotification(notification);
      await SendMailMessage(smtpClient, message, serverName, serverPort, cancellationToken);
    };
  }
}