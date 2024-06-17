
using MimeKit;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  static TNotification MapMessage<TNotification> (MimeMessage mailMessage) where TNotification: Notification =>
    (TNotification)CreateNotification (mailMessage.From[0].Name, mailMessage.To[0].Name, mailMessage.Subject, mailMessage.TextBody, mailMessage.Date);

  static MimeMessage MapNotification<TNotification> (TNotification notification) where TNotification: Notification =>
    BuildMailMessage (notification.From, notification.To, notification.Title, notification.Content, notification.Date);
}