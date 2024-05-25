
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static async Task SendMailMessage (
    SmtpClient smtpClient,
    MimeMessage mailMessage,
    SmtpClientOptions clientOptions,
    CancellationToken cancellationToken = default)
  {
    await smtpClient.ConnectAsync (clientOptions.ServerName, clientOptions.SewrverPort, false, cancellationToken);
    await smtpClient.SendAsync (mailMessage, cancellationToken);
    await smtpClient.DisconnectAsync (true, cancellationToken);
  }
}
