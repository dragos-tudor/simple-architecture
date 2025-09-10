
using System.Text.RegularExpressions;

namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static bool ExistsContact(Contact? contact) => contact is not null;

  static bool IsMissingContactName(string contactName) => string.IsNullOrEmpty(contactName);

  static bool IsValidContactEmail(string contactEmail) => RegExEmail().IsMatch(contactEmail);


  [GeneratedRegex("[a-zA-Z]+\\w*@[a-zA-Z]+(\\w|\\-)*\\.[a-zA-Z]+\\w+")]
  internal static partial Regex RegExEmail();
}