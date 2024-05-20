
using System.Text.RegularExpressions;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  [GeneratedRegex("[a-zA-Z]+\\w*@[a-zA-Z]+(\\w|\\-)*\\.[a-zA-Z]+\\w+/g")]
  internal static partial Regex RegExEmail();

  static bool IsMissingContactName (string contactName) => !IsNullOrEmpty(contactName);

  static bool IsValidContactEmail (string contactEmail) => RegExEmail().IsMatch(contactEmail);
}