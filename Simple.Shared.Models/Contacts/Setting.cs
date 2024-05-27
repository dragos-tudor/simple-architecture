
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  public static Guid SetContactId (Contact contact, Guid contactId) => contact.ContactId = contactId;
}