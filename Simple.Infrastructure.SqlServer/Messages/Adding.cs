
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Message AddMessage(AgendaContext dbContext, Message message) => SqlFuncs.AddEntity(dbContext, message);
}