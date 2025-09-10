
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static void SetContactPhoneNumber(Contact contact, PhoneNumber phoneNumber) => contact.PhoneNumbers.Add(phoneNumber);
}