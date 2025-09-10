
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  static Task DisconnectSmtpClientAsync(SmtpClient client, CancellationToken cancellationToken = default) => client.DisconnectAsync(true, cancellationToken);
}