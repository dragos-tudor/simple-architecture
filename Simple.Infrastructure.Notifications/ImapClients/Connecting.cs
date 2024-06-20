
using MailKit.Net.Imap;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static Task ConnectImapClientAsync (ImapClient imapClient, string serverName, int imapPort, CancellationToken cancellationToken = default) => imapClient.ConnectAsync(serverName, imapPort, false, cancellationToken);
}