
using System.Linq;

namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  internal static string GetMessageFrom(MimeMessage message) => string.Join(",", message.From.Select(m => m.Name));

  internal static string GetMessageFromName(MimeMessage message) => message.From[0].Name;

  internal static string GetMessageSubject(MimeMessage message) => message.Subject;

  internal static string GetMessageTo(MimeMessage message) => string.Join(",", message.To.Select(m => m.Name));
}