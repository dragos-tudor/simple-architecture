
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static ContactCreatedEvent CreateContactCreatedEvent (Guid contactId, string contactEmail) =>
    new (contactId, contactEmail);
}