
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static void AddMailMessage (ICollection<MimeMessage> mailMessages, MimeMessage mailMessage) => mailMessages.Add(mailMessage);
}