
using MailKit;
using MailKit.Net.Imap;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static Task<MimeMessage> GetMailFolderMessageAsync (ImapClient imapClient, IMailFolder mailFolder, UniqueId messageId, CancellationToken cancellationToken = default) =>
    mailFolder.GetMessageAsync(messageId, cancellationToken);

  static async Task<List<MimeMessage>> GetMailFolderMessagesAsync (ImapClient imapClient, IMailFolder mailFolder, IEnumerable<UniqueId> messageIds, CancellationToken cancellationToken)
  {
    var messages = new List<MimeMessage>();
    foreach (var messageId in messageIds)
      messages.Add(await GetMailFolderMessageAsync(imapClient, mailFolder, messageId, cancellationToken));
    return messages;
  }
}