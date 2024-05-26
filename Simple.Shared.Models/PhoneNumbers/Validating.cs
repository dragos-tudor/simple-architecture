
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  public static string? ValidatePhoneNumber (PhoneNumber phoneNumber) =>
    IsValidPhoneNumber(phoneNumber)? GetInvalidPhoneNumberError(phoneNumber.Number): default;

  public static IEnumerable<string?> ValidatePhoneNumbers (IEnumerable<PhoneNumber> phoneNumbers) =>
    phoneNumbers.Select(ValidatePhoneNumber).Where(ExistValidationError);
}