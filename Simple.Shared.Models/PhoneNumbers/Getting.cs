
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  static string GetInvalidPhoneNumberError (long number) => $"Invalid phone number {number}.";

  public static string GetDuplicatePhoneNumberError (long number) => $"Duplicate phone number {number}.";

  public static IEnumerable<string> GetDuplicatePhoneNumberErrors (IEnumerable<long> phoneNumbers) =>
    phoneNumbers.Select(GetDuplicatePhoneNumberError);
}