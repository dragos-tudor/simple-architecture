
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  static Task SendMailMessageAsync (SmtpClient client, MimeMessage message, CancellationToken cancellationToken = default) => client.SendAsync(message, cancellationToken);

  static async Task SendMailMessageAsync (SmtpClient client, MimeMessage message, string serverName, int smtpPort, CancellationToken cancellationToken = default)
  {
    if(!client.IsConnected)
      await ConnectSmtpClientAsync (client, serverName, smtpPort, cancellationToken);
    await AuthenticateSmtpClientAsync (client, cancellationToken);

    await SendMailMessageAsync(client, message, cancellationToken);
    message.Dispose();

    await DisconnectSmtpClientAsync (client, cancellationToken);
  }

  public static async Task SendMailAsync<TMail> (TMail mail, EmailServerOptions serverOptions, MapMail<TMail> mapMail, CancellationToken cancellationToken = default)
  {
    using var client = CreateSmtpClient();
    await SendMailMessageAsync(client, mapMail(mail), serverOptions.ContainerName, serverOptions.SmtpPort, cancellationToken);
  }
}
