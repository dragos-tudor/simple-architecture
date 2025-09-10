
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static Contact CreateContact(Guid contactId, string contactName, string contactEmail) =>
    new() { ContactId = contactId, ContactEmail = contactEmail, ContactName = contactName };
}