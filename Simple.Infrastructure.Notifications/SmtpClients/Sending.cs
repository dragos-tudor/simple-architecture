
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static async Task SendMailMessage (
    SmtpClient smtpClient,
    MimeMessage mailMessage,
    string serverName,
    int serverPort,
    CancellationToken cancellationToken = default)
  {
    await smtpClient.ConnectAsync (serverName, serverPort, false, cancellationToken);
    await smtpClient.SendAsync (mailMessage, cancellationToken);
    await smtpClient.DisconnectAsync (true, cancellationToken);
  }
}
