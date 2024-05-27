
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  static string? ValidateContactId (Guid contactId) => IsMissingContactId(contactId)? MissingContactIdError: default;

  static string? ValidateContactEmail (string contactEmail) => !IsValidContactEmail(contactEmail)? GetInvalidContactEmailError(contactEmail): default;

  static string? ValidateContactName (string contactName) => IsMissingContactName(contactName)? MissingContactNameError: default;

  public static IEnumerable<string> ValidateContact (Contact contact) => GetValidationErrors([
    ValidateContactId(contact.ContactId),
    ValidateContactEmail(contact.ContactEmail),
    ValidateContactName(contact.ContactName),
    .. ValidatePhoneNumbers(contact.PhoneNumbers ?? [])
  ]);

}