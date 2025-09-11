
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  static Task DisconnectSmtpClientAsync(SmtpClient client, CancellationToken cancellationToken = default) => client.DisconnectAsync(true, cancellationToken);
}