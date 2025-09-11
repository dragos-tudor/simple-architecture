
using MailKit.Net.Imap;

namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  static Task DisconnectImapClientAsync(ImapClient client, CancellationToken cancellationToken = default) => client.DisconnectAsync(true, cancellationToken);
}