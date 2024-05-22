using SqlFuncs = Storing.SqlServer.SqlServerFuncs;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static void AddMessage(AgendaContext dbContext, Message message) => SqlFuncs.AddEntity(dbContext, message);
}