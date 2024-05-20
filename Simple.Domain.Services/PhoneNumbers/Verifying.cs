
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  static bool IsMissingPhoneNumber (PhoneNumber phoneNumber) => !IsNullOrEmpty(phoneNumber.Number);
}