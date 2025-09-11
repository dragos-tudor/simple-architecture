
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  static Task AuthenticateSmtpClientAsync(SmtpClient client, string userName, string password, CancellationToken cancellationToken = default) => client.AuthenticateAsync(userName, password, cancellationToken);
}