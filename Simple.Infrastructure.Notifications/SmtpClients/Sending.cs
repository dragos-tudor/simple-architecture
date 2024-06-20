
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static async Task SendMailMessageAsync (SmtpClient smtpClient, MimeMessage message, CancellationToken cancellationToken = default)
  {
    await smtpClient.SendAsync(message, cancellationToken);
    message.Dispose();
  }

  public static async Task SendMailMessagesAsync (SmtpClient smtpClient, IEnumerable<MimeMessage> messages, string serverName, int smtpPort, CancellationToken cancellationToken = default)
  {
    if(!smtpClient.IsConnected)
      await ConnectSmtpClientAsync (smtpClient, serverName, smtpPort, cancellationToken);
    await AuthenticateSmtpClientAsync (smtpClient, cancellationToken);

    foreach(var message in messages) await SendMailMessageAsync(smtpClient, message, cancellationToken);
    await DisconnectSmtpClientAsync (smtpClient, cancellationToken);
  }
}
