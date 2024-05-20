using System.Net.Mail;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static SmtpClient CreateSmtpClient() => new ();
}