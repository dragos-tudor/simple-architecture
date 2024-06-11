
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task UpdateMessageIsActive (AgendaContext dbContext, Message message, bool isActive, CancellationToken cancellationToken = default)
  {
    UpdateEntity(dbContext, message, (message) => SetMessageIsActive(message, isActive));
    return SaveChanges(dbContext, cancellationToken);
  }
}