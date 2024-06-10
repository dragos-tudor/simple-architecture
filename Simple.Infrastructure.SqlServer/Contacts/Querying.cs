
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static IQueryable<Contact> FindContactByEmail (IQueryable<Contact> query, string contactEmail) =>
    query.Where(contact => contact.ContactEmail == contactEmail);

  public static IQueryable<Contact> FindContactByKey (IQueryable<Contact> query, Guid contactId) =>
    query.Where(contact => contact.ContactId == contactId);

  public static IQueryable<Contact> FindContactByName (IQueryable<Contact> query, string contactName) =>
    query.Where(contact => contact.ContactName == contactName);

  public static IQueryable<Contact> FindContacts (IQueryable<Contact> query, short? pageSize, short? pageIndex) =>
    query.Page(pageSize, pageIndex);
}