
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  static Task ConnectSmtpClientAsync(SmtpClient client, string serverName, int smtpPort, CancellationToken cancellationToken = default) => client.ConnectAsync(serverName, smtpPort, false, cancellationToken);
}