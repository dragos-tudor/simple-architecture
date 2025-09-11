
using MailKit.Net.Imap;

namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  public static ImapClient CreateImapClient() => new();
}