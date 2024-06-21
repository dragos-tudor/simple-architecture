
using MailKit.Net.Imap;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  static Task DisconnectImapClientAsync (ImapClient client, CancellationToken cancellationToken = default) => client.DisconnectAsync(true, cancellationToken);
}