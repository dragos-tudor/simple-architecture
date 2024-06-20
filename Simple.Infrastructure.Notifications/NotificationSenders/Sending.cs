
using System.Linq;
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static Task SendNotificationsAsync<TNotification> (
    SmtpClient smtpClient,
    IEnumerable<TNotification> notifications,
    EmailServerOptions emailServerOptions,
    Func<TNotification, MimeMessage> mapNotification,
    CancellationToken cancellationToken = default)
  {
    var messages = notifications.Select(mapNotification);
    return SendMailMessagesAsync(smtpClient, messages, emailServerOptions.ContainerName, emailServerOptions.SmtpPort, cancellationToken);
  }
}