
namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  static Guid SetContactId (Contact contact, Guid contactId) => contact.ContactId = contactId;
}