
using MimeKit;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  static MimeMessage MapNotification<TNotification>(TNotification notification) where TNotification : INotification =>
    BuildMailMessage(notification.From, notification.To, notification.Title, notification.Content, notification.Date);
}