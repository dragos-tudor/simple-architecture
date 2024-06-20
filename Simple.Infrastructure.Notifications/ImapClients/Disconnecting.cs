
using MailKit.Net.Imap;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static Task DisconnectImapClientAsync (ImapClient imapClient, CancellationToken cancellationToken = default) => imapClient.DisconnectAsync(true, cancellationToken);
}