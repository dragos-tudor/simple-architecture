
using MailKit;
using MailKit.Net.Imap;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  public static async Task<IEnumerable<MimeMessage>> ReceiveMailMessagesAsync(ImapClient client, string userName, string password, string serverName, int imapPort, CancellationToken cancellationToken = default)
  {
    if (!client.IsConnected)
      await ConnectImapClientAsync(client, serverName, imapPort, cancellationToken);
    await AuthenticateImapClientAsync(client, userName, password, cancellationToken);

    var inbox = await OpenInboxAsync(client, cancellationToken);
    var messageIds = await FindUnseenMessages(inbox, cancellationToken);
    var messages = await GetMailFolderMessagesAsync(inbox, messageIds, cancellationToken);
    await SetMailFolderMessagesFlagsAsync(inbox, messageIds, MessageFlags.Seen, cancellationToken);

    await DisconnectImapClientAsync(client, cancellationToken);
    return messages;
  }
}
