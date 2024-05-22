using SqlFuncs = Storing.SqlServer.SqlServerFuncs;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static void UpdateMessageIsActive (AgendaContext dbContext, Message message, bool isActive) =>
    SqlFuncs. UpdateEntity(dbContext, message, (message) => {
      message.IsActive = isActive;
    });
}