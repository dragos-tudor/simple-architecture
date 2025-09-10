
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public const string DuplicatePhoneNumberError = $"Duplicate phone number.";

  public static readonly string InvalidCountryCodeError = $"Invalid phone number country code. Should be between 0-{PhoneNumberContraints.MaxCountryCode}";

  public static readonly string InvalidExtensionError = $"Invalid phone number extension. Should be between 0-{PhoneNumberContraints.MaxExtension}";

  public static readonly string InvalidPhoneNumberError = $"Invalid phone number. Should be between 0-{PhoneNumberContraints.MaxNumber}";

  public const string MissingPhoneNumberError = $"Missing phone number.";
}