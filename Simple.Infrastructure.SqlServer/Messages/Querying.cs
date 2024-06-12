
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static IQueryable<Message> GetMessagesPage (IQueryable<Message> query, short? pageSize, short? pageIndex) =>
    query.Page(pageSize, pageIndex);
}