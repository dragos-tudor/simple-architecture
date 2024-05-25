
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  public static IEnumerable<string?> ValidatePhoneNumbers (IEnumerable<PhoneNumber> phoneNumbers) =>
    phoneNumbers.Select(ValidatePhoneNumber).Where(ExistValidationError);

  public static string? ValidatePhoneNumber (PhoneNumber phoneNumber) =>
    IsMissingPhoneNumber(phoneNumber)? GetMissingPhoneNumberError(): default;
}