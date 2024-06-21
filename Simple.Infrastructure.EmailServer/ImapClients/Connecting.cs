
using MailKit.Net.Imap;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  static Task ConnectImapClientAsync (ImapClient client, string serverName, int imapPort, CancellationToken cancellationToken = default) => client.ConnectAsync(serverName, imapPort, false, cancellationToken);
}