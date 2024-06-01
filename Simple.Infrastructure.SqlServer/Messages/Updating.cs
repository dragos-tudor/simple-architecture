
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Message UpdateMessageIsActive (AgendaContext dbContext, Message message, bool isActive) =>
    UpdateEntity(dbContext, message, (message) => message.IsActive = isActive );
}