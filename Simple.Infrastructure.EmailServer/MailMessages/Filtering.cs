
using System.Linq;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  static IEnumerable<TMail> FilterMailMessages<TMail> (IEnumerable<MimeMessage> messages, MapMessage<TMail> mapMessage, Predicate<TMail> filterMail) =>
    messages.Select(message => mapMessage(message)).Where(mail => filterMail(mail));
}