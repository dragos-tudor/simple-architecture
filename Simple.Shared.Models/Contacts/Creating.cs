
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  public static Contact CreateContact (
    Guid contactId,
    string contactName,
    string contactEmail,
    IList<PhoneNumber>? phoneNumbers = default)
  =>
    new () {
      ContactId = contactId,
      ContactEmail = contactEmail,
      ContactName = contactName,
      PhoneNumbers = phoneNumbers ?? []
    };
}