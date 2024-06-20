
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static Task DisconnectSmtpClientAsync (SmtpClient smtpClient, CancellationToken cancellationToken = default) =>
    smtpClient.DisconnectAsync(true, cancellationToken);
}