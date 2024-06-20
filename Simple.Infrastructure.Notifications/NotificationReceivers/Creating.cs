#pragma warning disable CA2000

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static ReceiveNotifications<TNotification> CreateNotificationsReceiver<TNotification> (
    EmailServerOptions emailServerOptions,
    Func<MimeMessage, TNotification> mapMessage)
  {
    var imapClient = CreateImapClient();
    return (userName, password, filterNotificaiton, cancellationToken) => ReceiveNotificationsAsync(imapClient, userName, password, emailServerOptions, mapMessage, filterNotificaiton, cancellationToken);
  }
}