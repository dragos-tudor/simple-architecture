
using System.Threading;
using MailKit.Net.Smtp;
using MimeKit;

namespace Simple.Architecture.Notifications;

partial class NotificationsFuncs
{
  public static async Task SendEmail(
    SmtpClient smtpClient,
    MimeMessage message,
    MailServerConfiguration serverConfiguration,
    CancellationToken cancellationToken = default)
  {
    await smtpClient.ConnectAsync (serverConfiguration.Hostname, serverConfiguration.Port, false, cancellationToken);
    await smtpClient.SendAsync (message, cancellationToken);
    await smtpClient.DisconnectAsync (true, cancellationToken);
  }
}
