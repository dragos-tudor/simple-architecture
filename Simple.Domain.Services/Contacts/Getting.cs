
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static string GetInvalidContactEmailError (string contactEmail) => $"Invalid contact email '{contactEmail}'.";

  public static string GetMissingContactError (Guid contactId) => $"Missing contact '{contactId}'.";

  public const string MissingContactIdError = "Missing contact id.";

  public const string MissingContactNameError = "Missing contact name.";
}