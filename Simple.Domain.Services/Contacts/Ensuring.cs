
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static Contact EnsureContactPhoneNumbers(Contact contact) =>
    contact.PhoneNumbers is null? contact with { PhoneNumbers = [] }: contact;
}