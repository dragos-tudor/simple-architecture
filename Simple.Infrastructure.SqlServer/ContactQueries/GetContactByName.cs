
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static IQueryable<Contact> GetContactByName (IQueryable<Contact> query, string contactName) =>
    query.Where(contact => contact.ContactName == contactName);
}