
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  static string? ValidateContactEmail (string contactEmail) => !IsValidContactEmail(contactEmail)? GetInvalidContactEmailError(contactEmail): default;

  static string? ValidateContactName (string contactName) => IsMissingContactName(contactName)? GetMissingContactNameError(): default;

  public static IEnumerable<string> ValidateContact (Contact contact) =>
    GetValidationErrors([
      ValidateContactEmail(contact.ContactEmail),
      ValidateContactName(contact.ContactName),
      .. ValidatePhoneNumbers(contact.PhoneNumbers ?? [])
    ]);

}