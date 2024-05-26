
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  static bool IsValidPhoneNumber (PhoneNumber phoneNumber) => phoneNumber.Number <= PhoneNumberContraints.MaxNumber;

  public static bool ExistsPhoneNumber (long? phoneNumber) => phoneNumber is not null;

  public static bool ExistsPhoneNumbers (IEnumerable<long> phoneNumbers) => phoneNumbers.Any();
}