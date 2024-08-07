
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  static Task AuthenticateSmtpClientAsync (SmtpClient client, string userName, string password, CancellationToken cancellationToken = default) => client.AuthenticateAsync(userName, password, cancellationToken);
}