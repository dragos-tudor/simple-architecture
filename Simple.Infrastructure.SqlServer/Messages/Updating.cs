
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task UpdateMessageIsActiveAsync (AgendaContext dbContext, Message message, bool isActive, CancellationToken cancellationToken = default)
  {
    UpdateEntity(dbContext, message, (message) => SetMessageIsActive(message, isActive));
    return SaveChanges(dbContext, cancellationToken);
  }

  public static Task UpdateMessageFailureAsync (AgendaContext dbContext, Message message, string failureMessage, byte failureCounter, bool isActive = true, CancellationToken cancellationToken = default)
  {
    UpdateEntity(dbContext, message, (message) => {
      SetMessageFailureMessage(message, failureMessage);
      SetMessageFailureCounter(message, failureCounter);
      SetMessageIsActive(message, isActive);
    });
    return SaveChanges(dbContext, cancellationToken);
  }
}