
using MailKit;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  static Task<MimeMessage> GetMailFolderMessageAsync (IMailFolder mailFolder, UniqueId messageId, CancellationToken cancellationToken = default) =>
    mailFolder.GetMessageAsync(messageId, cancellationToken);

  static async Task<List<MimeMessage>> GetMailFolderMessagesAsync (IMailFolder mailFolder, IEnumerable<UniqueId> messageIds, CancellationToken cancellationToken)
  {
    var messages = new List<MimeMessage>();
    foreach (var messageId in messageIds)
      messages.Add(await GetMailFolderMessageAsync(mailFolder, messageId, cancellationToken));
    return messages;
  }
}