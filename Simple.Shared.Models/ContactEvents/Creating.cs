
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  public static ContactCreatedEvent CreateContactCreatedEvent (Guid contactId, string contactEmail) =>
    new (contactId, contactEmail);
}