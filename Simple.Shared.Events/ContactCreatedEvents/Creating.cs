
namespace Simple.Shared.Events;

partial class EventsFuncs
{
  public static ContactCreatedEvent CreateContactCreatedEvent(Guid contactId, string contactEmail) =>
    new(contactId, contactEmail);
}
