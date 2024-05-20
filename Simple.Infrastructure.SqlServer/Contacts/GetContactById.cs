
namespace Simple.Domain.Services;

partial class DomainFuncs
{
  public static IQueryable<Contact> GetContactByIdSpec (IQueryable<Contact> query, Guid contactId) =>
    query.Where(contact => contact.ContactId == contactId);
}