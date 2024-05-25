
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  static bool IsMissingPhoneNumber (PhoneNumber phoneNumber) => string.IsNullOrEmpty(phoneNumber.Number);

  public static bool ExistsPhoneNumbers (IEnumerable<string> phoneNumbers) => phoneNumbers.Any();
}