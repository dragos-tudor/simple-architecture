
namespace Simple.Architecture.Domain;

partial class DomainFuncs
{
  public static IEnumerable<PhoneNumber> AddPhoneNumber (Contact contact, PhoneNumber phoneNumber) =>
    contact.PhoneNumbers = [ .. contact.PhoneNumbers ?? [], phoneNumber];
}