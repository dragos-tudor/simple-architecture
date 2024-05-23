
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Message UpdateMessageIsActive (AgendaContext dbContext, Message message, bool isActive) =>
    SqlFuncs. UpdateEntity(dbContext, message, (message) => {
      message.IsActive = isActive;
    });
}