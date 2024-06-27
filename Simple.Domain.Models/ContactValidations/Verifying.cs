
using System.Text.RegularExpressions;

namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  [GeneratedRegex("[a-zA-Z]+\\w*@[a-zA-Z]+(\\w|\\-)*\\.[a-zA-Z]+\\w+")]
  internal static partial Regex RegExEmail();

  static bool IsMissingContactName (string contactName) => string.IsNullOrEmpty(contactName);

  static bool IsValidContactEmail (string contactEmail) => RegExEmail().IsMatch(contactEmail);
}