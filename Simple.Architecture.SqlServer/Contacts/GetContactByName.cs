
namespace Simple.Architecture.Domain;

partial class DomainFuncs
{
  public static IQueryable<Contact> GetContactByNameSpec (IQueryable<Contact> query, string contactName) =>
    query.Where(contact => contact.ContactName == contactName);
}