
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  internal static MimeMessage? GetMailMessage (string subject, IEnumerable<MimeMessage>? mailMessages = default) =>
    (mailMessages ?? MailMessages).FirstOrDefault(message => message.Subject == subject);
}
