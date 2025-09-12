
using MailKit;
using MailKit.Net.Imap;

namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  public static async Task<IEnumerable<MimeMessage>> ReceiveMailMessagesAsync(ImapClient client, string userName, string password, MailServerOptions options, CancellationToken cancellationToken = default)
  {
    if (!client.IsConnected)
      await ConnectImapClientAsync(client, options.MailServerName, options.ImapPort, cancellationToken);
    await AuthenticateImapClientAsync(client, userName, password, cancellationToken);

    var inbox = await OpenInboxAsync(client, cancellationToken);
    var messageIds = await FindUnseenMessages(inbox, cancellationToken);
    var messages = await GetMailFolderMessagesAsync(inbox, messageIds, cancellationToken);
    await SetMailFolderMessagesFlagsAsync(inbox, messageIds, MessageFlags.Seen, cancellationToken);

    await DisconnectImapClientAsync(client, cancellationToken);
    return messages;
  }
}
