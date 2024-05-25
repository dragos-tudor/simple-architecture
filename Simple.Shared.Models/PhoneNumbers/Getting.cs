
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  static string GetMissingPhoneNumberError () => "Missing phone number.";

  public static string GetDuplicatePhoneNumberError (string number) => $"Duplicate phone number {number}.";

  public static IEnumerable<string> GetDuplicatePhoneNumberErrors (IEnumerable<string> phoneNumbers) =>
    phoneNumbers.Select(GetDuplicatePhoneNumberError);
}