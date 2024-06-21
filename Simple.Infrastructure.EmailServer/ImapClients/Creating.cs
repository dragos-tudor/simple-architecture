
using MailKit.Net.Imap;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  public static ImapClient CreateImapClient () => new ();
}