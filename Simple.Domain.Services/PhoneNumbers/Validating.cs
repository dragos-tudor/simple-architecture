
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  static IEnumerable<string?> ValidatePhoneNumbers (IEnumerable<PhoneNumber> phoneNumbers) =>
    phoneNumbers.Select(ValidatePhoneNumber).Where(ExistValidationError);

  public static string? ValidatePhoneNumber (PhoneNumber phoneNumber) =>
    IsMissingPhoneNumber(phoneNumber)? default: GetMissingPhoneNumberError();
}