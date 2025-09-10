
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  static string? ValidateContactEmail(string contactEmail) => !IsValidContactEmail(contactEmail) ? InvalidContactEmailError : default;

  static string? ValidateContactName(string contactName) => IsMissingContactName(contactName) ? MissingContactNameError : default;

  public static IEnumerable<string?> ValidateContact(Contact contact) => [
    ValidateContactEmail(contact.ContactEmail),
    ValidateContactName(contact.ContactName),
  ];
}