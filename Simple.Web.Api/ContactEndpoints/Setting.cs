
namespace Simple.Web.Api;

partial class ApiFuncs
{
  static Guid SetContactId (Contact contact, Guid contactId) => contact.ContactId = contactId;
}