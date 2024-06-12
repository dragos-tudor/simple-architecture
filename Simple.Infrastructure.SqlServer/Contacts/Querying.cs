
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static IQueryable<Contact> GetContactsPage (IQueryable<Contact> query, short? pageSize, short? pageIndex) =>
    query.Page(pageSize, pageIndex);
}