
namespace Simple.Architecture.Domain;

partial class DomainFuncs
{
  static string? ValidateContactEmail (string contactEmail) => IsValidContactEmail(contactEmail)? default: GetInvalidContactEmailError(contactEmail);

  static string? ValidateContactName (string contactName) => IsMissingContactName(contactName)? default: GetMissingContactNameError();

  public static IEnumerable<string> ValidateContact (Contact contact) =>
    GetValidationErrors([
      ValidateContactEmail(contact.ContactEmail),
      ValidateContactName(contact.ContactName),
      .. ValidatePhoneNumbers(contact.PhoneNumbers ?? [])
    ]);

}