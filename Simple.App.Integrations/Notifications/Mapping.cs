
using MimeKit;

namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  public static TNotification MapMessage<TNotification> (MimeMessage mailMessage) where TNotification: Notification =>
    (TNotification)CreateNotification (mailMessage.From[0].Name, mailMessage.To[0].Name, mailMessage.Subject, mailMessage.TextBody, mailMessage.Date);

  public static MimeMessage MapNotification<TNotification> (TNotification notification) where TNotification: Notification =>
    BuildMailMessage (notification.From, notification.To, notification.Title, notification.Content, notification.Date);
}