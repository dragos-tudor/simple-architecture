
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Message UpdateMessage(AgendaContext dbContext, Message message) => UpdateEntity(dbContext, message, (message) => { });

  public static Message UpdateMessage(AgendaContext dbContext, Message message, Action<Message> updateMessage) => UpdateEntity(dbContext, message, updateMessage);
}