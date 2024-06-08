
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  static ContactValidationFailure? ValidateContactEmail (string contactEmail) => !IsValidContactEmail(contactEmail)? GetInvalidContactEmailFailure(contactEmail): default;

  static ContactValidationFailure? ValidateContactName (string contactName) => IsMissingContactName(contactName)? GetMissingContactNameFailure(): default;

  public static IEnumerable<ContactValidationFailure?> ValidateContact (Contact contact) => [
    ValidateContactEmail(contact.ContactEmail),
    ValidateContactName(contact.ContactName),
  ];
}