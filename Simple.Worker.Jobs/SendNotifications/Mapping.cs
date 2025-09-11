
using MimeKit;

namespace Simple.Worker.Jobs;

partial class JobsFuncs
{
  static MimeMessage MapNotification<TNotification>(TNotification notification) where TNotification : INotification =>
    BuildMailMessage(notification.From, notification.To, notification.Title, notification.Content, notification.Date);
}