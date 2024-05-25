
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static MimeMessage BuildMailMessage (string to, string from, string subject, string body)
  {
    var mailMessage = CreateMailMessage();
    SetMessageFrom(mailMessage, new MailboxAddress(from, from));
    SetMessageTo(mailMessage, new MailboxAddress(to, to));
    SetMessageSubject(mailMessage, subject);
    SetMessageBody(mailMessage, body);
    return mailMessage;
  }
}