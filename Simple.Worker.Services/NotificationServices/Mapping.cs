
using MimeKit;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  static MimeMessage MapNotification<TNotification> (TNotification notification) where TNotification: INotification =>
    BuildMailMessage (notification.From, notification.To, notification.Title, notification.Content, notification.Date);
}