
using MailKit.Net.Imap;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static Task AuthenticateImapClientAsync (ImapClient imapClient, string userName, string password, CancellationToken cancellationToken = default) => imapClient.AuthenticateAsync(userName, password, cancellationToken);
}