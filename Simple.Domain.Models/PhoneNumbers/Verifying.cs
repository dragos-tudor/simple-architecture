
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static bool ExistsPhoneNumber (PhoneNumber? phoneNumber) => phoneNumber is not null;

  public static bool ExistPhoneNumbers (IEnumerable<PhoneNumber> phoneNumbers) => phoneNumbers?.Any() ?? false;
}