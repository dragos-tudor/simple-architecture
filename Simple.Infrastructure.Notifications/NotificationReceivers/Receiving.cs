
using System.Linq;
using MailKit.Net.Imap;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static async Task<IEnumerable<TNotification>> ReceiveNotificationsAsync<TNotification> (
    ImapClient imapClient,
    string userName,
    string password,
    EmailServerOptions emailServerOptions,
    Func<MimeMessage, TNotification> mapMessage,
    Predicate<TNotification> filterNotification,
    CancellationToken cancellationToken = default)
  {
    var messages = await ReceiveMailMessagesAsync(imapClient, userName, password, emailServerOptions.ContainerName, emailServerOptions.ImapPort, cancellationToken);
    return messages.Select(mapMessage).Where(notification => filterNotification(notification)).ToList();
  }
}