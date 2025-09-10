
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  static Task<string> SendMailMessageAsync(SmtpClient client, MimeMessage message, CancellationToken cancellationToken = default) => client.SendAsync(message, cancellationToken);

  public static async Task SendMailMessageAsync(SmtpClient client, MimeMessage message, string serverName, int smtpPort, CancellationToken cancellationToken = default)
  {
    if (!client.IsConnected)
      await ConnectSmtpClientAsync(client, serverName, smtpPort, cancellationToken);
    await AuthenticateSmtpClientAsync(client, GetMessageFromName(message), GetMessageFromName(message), cancellationToken);

    await SendMailMessageAsync(client, message, cancellationToken);
    message.Dispose();

    await DisconnectSmtpClientAsync(client, cancellationToken);
  }
}
