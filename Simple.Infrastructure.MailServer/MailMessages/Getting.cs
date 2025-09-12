
using System.Linq;

namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  public static string GetMessageFrom(MimeMessage message) => string.Join(",", message.From.Select(m => m.Name));

  public static string GetMessageFromName(MimeMessage message) => message.From[0].Name;

  public static string GetMessageSubject(MimeMessage message) => message.Subject;

  public static string GetMessageTo(MimeMessage message) => string.Join(",", message.To.Select(m => m.Name));
}