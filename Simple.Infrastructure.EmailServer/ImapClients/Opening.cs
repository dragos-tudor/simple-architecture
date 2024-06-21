
using MailKit;
using MailKit.Net.Imap;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  static async Task<IMailFolder> OpenInboxAsync (ImapClient client, CancellationToken cancellationToken = default)
  {
    await client.Inbox.OpenAsync(FolderAccess.ReadWrite, cancellationToken);
    return client.Inbox;
  }
}