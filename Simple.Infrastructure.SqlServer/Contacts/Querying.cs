
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static IQueryable<Contact> FindContactByKey (IQueryable<Contact> query, Guid contactId) =>
    query.Where(contact => contact.ContactId == contactId);

  public static IQueryable<Contact> FindContactByName (IQueryable<Contact> query, string contactName) =>
    query.Where(contact => contact.ContactName == contactName);
}