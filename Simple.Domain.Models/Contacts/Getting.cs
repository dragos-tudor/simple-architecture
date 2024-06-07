#pragma warning disable CA1024

namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static ContactValidationException GetInvalidContactEmailError (string contactEmail) => new ($"Invalid contact email '{contactEmail}'.");

  public static ContactValidationException GetMissingContactError (Guid contactId) => new ($"Missing contact {contactId}.");

  public static ContactValidationException GetMissingContactNameError () => new ("Missing contact name.");

  public static ContactDuplicateException GetDuplicateContactNameError (string contactName) => new ($"Duplicate contact name {contactName}.");

  public static ContactDuplicateException GetDuplicateContactEmailError (string contactEmail) => new ($"Duplicate contact email {contactEmail}.");
}