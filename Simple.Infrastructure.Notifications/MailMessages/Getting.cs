
using System.Linq;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  internal static string GetMessageFrom (MimeMessage message) => string.Join(",", message.From.Select(m => m.Name));

  internal static string GetMessageSubject (MimeMessage message) => message.Subject;

  internal static string GetMessageTo (MimeMessage message) => string.Join(",", message.To.Select(m => m.Name));
}