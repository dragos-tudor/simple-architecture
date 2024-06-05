
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  internal static TNotification MapMessage<TNotification> (MimeMessage mailMessage) where TNotification: Notification =>
    (TNotification)CreateNotification (mailMessage.To[0].Name, mailMessage.From[0].Name, mailMessage.Subject, mailMessage.TextBody, mailMessage.Date);

  internal static MimeMessage MapNotification<TNotification> (TNotification notification) where TNotification: Notification =>
    BuildMailMessage (notification.From, notification.To, notification.Title, notification.Content, notification.Date);
}