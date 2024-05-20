
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static IEnumerable<PhoneNumber> AddPhoneNumber (Contact contact, PhoneNumber phoneNumber) =>
    contact.PhoneNumbers = [ .. contact.PhoneNumbers ?? [], phoneNumber];
}