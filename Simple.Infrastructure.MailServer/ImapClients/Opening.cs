
using MailKit;
using MailKit.Net.Imap;

namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  static async Task<IMailFolder> OpenInboxAsync(ImapClient client, CancellationToken cancellationToken = default)
  {
    await client.Inbox.OpenAsync(FolderAccess.ReadWrite, cancellationToken);
    return client.Inbox;
  }
}