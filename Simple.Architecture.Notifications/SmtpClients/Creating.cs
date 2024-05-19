using System.Net.Mail;

namespace Simple.Architecture.Notifications;

partial class NotificationsFuncs
{
  public static SmtpClient CreateSmtpClient() => new ();
}