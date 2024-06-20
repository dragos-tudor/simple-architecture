
using MailKit;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static Task SetMailFolderMessageFlagsAsync (IMailFolder mailFolder, UniqueId messageId, MessageFlags messageFlags, CancellationToken cancellationToken = default) =>
    mailFolder.AddFlagsAsync(messageId, messageFlags, true, cancellationToken);

  static async Task SetMailFolderMessagesFlagsAsync (IMailFolder mailFolder, IEnumerable<UniqueId> messageIds, MessageFlags messageFlags, CancellationToken cancellationToken = default)
  {
    foreach(var messageId in messageIds)
      await SetMailFolderMessageFlagsAsync(mailFolder, messageId, messageFlags, cancellationToken);
  }
}