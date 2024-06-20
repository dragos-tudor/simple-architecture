
using MailKit.Net.Imap;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static ImapClient CreateImapClient () => new ();
}