using MimeKit;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static MimeMessage BuildMimeMessage(string to, string from, string subject, string body)
  {
    var message = CreateMimeMessage();
    SetMessageFrom(message, new MailboxAddress(from, from));
    SetMessageTo(message, new MailboxAddress(to, to));
    SetMessageSubject(message, subject);
    SetMessageBody(message, body);
    return message;
  }
}