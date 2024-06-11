
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  internal static Message AddMessage(AgendaContext dbContext, Message message) => AddEntity(dbContext, message);
}