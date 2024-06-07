
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  static ContactValidationException? ValidateContactEmail (string contactEmail) => !IsValidContactEmail(contactEmail)? GetInvalidContactEmailError(contactEmail): default;

  static ContactValidationException? ValidateContactName (string contactName) => IsMissingContactName(contactName)? GetMissingContactNameError(): default;

  public static IEnumerable<ContactValidationException?> ValidateContact (Contact contact) => [
    ValidateContactEmail(contact.ContactEmail),
    ValidateContactName(contact.ContactName),
  ];
}