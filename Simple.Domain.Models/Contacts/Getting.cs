#pragma warning disable CA1024

namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static ContactValidationFailure GetInvalidContactEmailFailure (string contactEmail) => new ($"Invalid contact email '{contactEmail}'.");

  public static ContactValidationFailure GetMissingContactFailure (Guid contactId) => new ($"Missing contact {contactId}.");

  public static ContactValidationFailure GetMissingContactNameFailure () => new ("Missing contact name.");

  public static ContactDuplicateFailure GetDuplicateContactNameFailure (string contactName) => new ($"Duplicate contact name {contactName}.");

  public static ContactDuplicateFailure GetDuplicateContactEmailFailure (string contactEmail) => new ($"Duplicate contact email {contactEmail}.");
}