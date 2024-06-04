
namespace Simple.Domain.Services;

public record ContactCreatedEvent(Guid ContactId, string ContactEmail);

partial class ServicesFuncs
{
  public static ContactCreatedEvent CreateContactCreatedEvent (Guid contactId, string contactEmail) =>
    new (contactId, contactEmail);
}
