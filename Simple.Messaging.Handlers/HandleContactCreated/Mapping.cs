
using MimeKit;

namespace Simple.Messaging.Handlers;

partial class HandlersFuncs
{
  static MimeMessage ToMimeMessage(NotificationSentEvent @event) =>
    BuildMailMessage(@event.From, @event.To, @event.Subject, @event.Content, @event.Date);
}