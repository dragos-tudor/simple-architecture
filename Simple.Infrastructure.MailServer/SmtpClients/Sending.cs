
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  static Task<string> SendMailMessageAsync(SmtpClient client, MimeMessage message, CancellationToken cancellationToken = default) => client.SendAsync(message, cancellationToken);

  public static async Task SendMailMessageAsync(SmtpClient client, MimeMessage message, MailServerOptions options, CancellationToken cancellationToken = default)
  {
    if (!client.IsConnected)
      await ConnectSmtpClientAsync(client, options.MailServerName, options.SmtpPort, cancellationToken);
    await AuthenticateSmtpClientAsync(client, GetMessageFromName(message), GetMessageFromName(message), cancellationToken);

    await SendMailMessageAsync(client, message, cancellationToken);
    await DisconnectSmtpClientAsync(client, cancellationToken);
  }
}
