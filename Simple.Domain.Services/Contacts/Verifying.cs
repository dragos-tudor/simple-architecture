
using System.Text.RegularExpressions;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  [GeneratedRegex("[a-zA-Z]+\\w*@[a-zA-Z]+(\\w|\\-)*\\.[a-zA-Z]+\\w+")]
  internal static partial Regex RegExEmail();

  static bool IsMissingContactId (Guid contactId) => contactId == Guid.Empty;

  static bool IsMissingContactName (string contactName) => string.IsNullOrEmpty(contactName);

  static bool IsValidContactEmail (string contactEmail) => RegExEmail().IsMatch(contactEmail);
}