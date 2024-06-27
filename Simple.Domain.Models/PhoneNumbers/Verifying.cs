
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static bool ExistPhoneNumber (PhoneNumber? phoneNumber) => phoneNumber is not null;

  public static bool ExistsPhoneNumbers (IEnumerable<PhoneNumber> phoneNumbers) => phoneNumbers?.Any() ?? false;
}