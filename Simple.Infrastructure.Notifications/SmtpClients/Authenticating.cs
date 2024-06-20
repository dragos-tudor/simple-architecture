
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static Task AuthenticateSmtpClientAsync (SmtpClient smtpClient, CancellationToken cancellationToken = default) => smtpClient.AuthenticateAsync("test", "test", cancellationToken);
}