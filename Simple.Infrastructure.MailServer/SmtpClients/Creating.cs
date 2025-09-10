
using MailKit.Net.Smtp;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  public static SmtpClient CreateSmtpClient() => new();
}