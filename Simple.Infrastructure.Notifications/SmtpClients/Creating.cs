
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static SmtpClient CreateSmtpClient () => new ();
}