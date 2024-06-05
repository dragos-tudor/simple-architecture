
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static MimeEntity SetMessageBody (MimeMessage message, string body) => message.Body = new TextPart("plain") { Text = body };

  static void SetMessageFrom (MimeMessage message, MailboxAddress address) => message.From.Add(address);

  static void SetMessageSubject (MimeMessage message, string subject) => message.Subject = subject;

  static void SetMessageTo (MimeMessage message, MailboxAddress address) => message.To.Add(address);

  static DateTimeOffset SetMessageDate (MimeMessage message, DateTimeOffset date) => message.Date = date;
}