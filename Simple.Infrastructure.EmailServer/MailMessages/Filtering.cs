
using System.Linq;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  public static IEnumerable<TMail> FilterMailMessages<TMail> (IEnumerable<MimeMessage> messages, Func<MimeMessage, TMail> mapMessage, Predicate<TMail> filterMail) =>
    messages.Select(message => mapMessage(message)).Where(mail => filterMail(mail));
}