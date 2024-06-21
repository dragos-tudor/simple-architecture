
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  static Task AuthenticateSmtpClientAsync (SmtpClient client, CancellationToken cancellationToken = default) => client.AuthenticateAsync("test", "test", cancellationToken);
}