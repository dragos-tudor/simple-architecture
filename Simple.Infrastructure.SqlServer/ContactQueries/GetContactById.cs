
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static IQueryable<Contact> GetContactById (IQueryable<Contact> query, Guid contactId) =>
    query.Where(contact => contact.ContactId == contactId);
}