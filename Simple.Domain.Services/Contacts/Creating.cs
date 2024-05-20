
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static Contact CreateContact (
    Guid contactId,
    string contactName,
    string contactEmail,
    IEnumerable<PhoneNumber>? phoneNumbers = default)
  =>
    new () {
      ContactId = contactId,
      ContactEmail = contactEmail,
      ContactName = contactName,
      PhoneNumbers = phoneNumbers
    };
}