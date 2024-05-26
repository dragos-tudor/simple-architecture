
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static async Task SendMailMessage (
    SmtpClient smtpClient,
    MimeMessage mailMessage,
    SmtpServerOptions serverOptions,
    CancellationToken cancellationToken = default)
  {
    await smtpClient.ConnectAsync (serverOptions.ServerName, serverOptions.SewrverPort, false, cancellationToken);
    LogSendingMessageError(Logger, mailMessage.Subject, ConcatMailAdresses(GetMailAddresses(mailMessage.To)));
    await smtpClient.SendAsync (mailMessage, cancellationToken);
    await smtpClient.DisconnectAsync (true, cancellationToken);
  }
}
