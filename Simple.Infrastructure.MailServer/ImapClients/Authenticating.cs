
using MailKit.Net.Imap;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  static Task AuthenticateImapClientAsync(ImapClient client, string userName, string password, CancellationToken cancellationToken = default) => client.AuthenticateAsync(userName, password, cancellationToken);
}