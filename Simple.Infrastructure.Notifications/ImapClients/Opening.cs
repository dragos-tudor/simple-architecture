
using MailKit;
using MailKit.Net.Imap;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static async Task<IMailFolder> OpenInboxFolderAsync (ImapClient imapClient, CancellationToken cancellationToken = default)
  {
    var clientInbox = imapClient.Inbox;
    await clientInbox.OpenAsync(FolderAccess.ReadWrite, cancellationToken);
    return clientInbox;
  }
}