
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static Task ConnectSmtpClientAsync (SmtpClient smtpClient, string serverName, int smtpPort, CancellationToken cancellationToken = default) =>
    smtpClient.ConnectAsync(serverName, smtpPort, false, cancellationToken);
}