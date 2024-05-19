
namespace Simple.Architecture.Domain;

partial class DomainFuncs
{
  public static ContactCreatedEvent CreateContactCreatedEvent (Guid contactId, string contactEmail) =>
    new (contactId, contactEmail);
}