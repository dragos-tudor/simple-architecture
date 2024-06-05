
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static MimeMessage? GetMailMessage (ICollection<MimeMessage> messages, string subject) => messages.FirstOrDefault(message => message.Subject == subject);
}