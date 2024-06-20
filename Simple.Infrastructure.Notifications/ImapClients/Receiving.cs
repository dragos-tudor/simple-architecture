
using MailKit;
using MailKit.Net.Imap;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static async Task<IEnumerable<MimeMessage>> ReceiveMailMessagesAsync (
    ImapClient imapClient,
    string userName,
    string password,
    string serverName,
    int imapPort,
    CancellationToken cancellationToken = default)
  {
    if (!imapClient.IsConnected)
      await ConnectImapClientAsync(imapClient, serverName, imapPort, cancellationToken);
    await AuthenticateImapClientAsync(imapClient, userName, password, cancellationToken);

    var inboxFolder = await OpenInboxFolderAsync(imapClient, cancellationToken);
    var messageIds = await FindUnseenMessages(inboxFolder, cancellationToken);
    var messages = await GetMailFolderMessagesAsync(imapClient, inboxFolder, messageIds, cancellationToken);
    await SetMailFolderMessagesFlagsAsync(inboxFolder, messageIds, MessageFlags.Seen, cancellationToken);

    await DisconnectImapClientAsync(imapClient, cancellationToken);
    return messages;
  }
}
