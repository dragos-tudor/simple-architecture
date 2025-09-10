
namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  public static MimeMessage BuildMailMessage(string from, string to, string subject, string body, DateTimeOffset? date = default)
  {
    var mailMessage = new MimeMessage();

    SetMessageFrom(mailMessage, new MailboxAddress(from, from));
    SetMessageTo(mailMessage, new MailboxAddress(to, to));
    SetMessageSubject(mailMessage, subject);
    SetMessageBody(mailMessage, body);
    SetMessageDate(mailMessage, date ?? DateTimeOffset.UtcNow);

    return mailMessage;
  }
}