
using System.Linq;

namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  public static bool IsMessageFrom (MimeMessage message, string from) => message.From.Any(address => address.Name == from);

  public static bool IsMessageTo (MimeMessage message, string to) => message.To.Any(address => address.Name == to);
}