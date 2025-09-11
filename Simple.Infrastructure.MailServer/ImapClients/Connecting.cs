
using MailKit.Net.Imap;

namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  static Task ConnectImapClientAsync(ImapClient client, string serverName, int imapPort, CancellationToken cancellationToken = default) => client.ConnectAsync(serverName, imapPort, false, cancellationToken);
}