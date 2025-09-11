
using MailKit.Net.Imap;

namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  static Task AuthenticateImapClientAsync(ImapClient client, string userName, string password, CancellationToken cancellationToken = default) => client.AuthenticateAsync(userName, password, cancellationToken);
}