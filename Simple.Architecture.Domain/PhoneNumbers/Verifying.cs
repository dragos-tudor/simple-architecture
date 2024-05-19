
namespace Simple.Architecture.Domain;

partial class DomainFuncs
{
  static bool IsMissingPhoneNumber (PhoneNumber phoneNumber) => !IsNullOrEmpty(phoneNumber.Number);
}