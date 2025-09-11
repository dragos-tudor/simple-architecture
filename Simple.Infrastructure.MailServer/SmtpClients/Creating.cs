
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  public static SmtpClient CreateSmtpClient() => new();
}