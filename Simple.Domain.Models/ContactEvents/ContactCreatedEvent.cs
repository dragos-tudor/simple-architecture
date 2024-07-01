
namespace Simple.Domain.Models;

public record ContactCreatedEvent(Guid ContactId, string ContactEmail): IEvent;

partial class ModelsFuncs
{
  public static ContactCreatedEvent CreateContactCreatedEvent (Guid contactId, string contactEmail) =>
    new (contactId, contactEmail);
}
