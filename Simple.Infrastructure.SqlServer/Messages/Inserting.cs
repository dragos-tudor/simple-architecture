
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task InsertMessage (AgendaContext dbContext, Message message, CancellationToken cancellationToken = default)
  {
    AddMessage(dbContext, message);
    return SaveChanges(dbContext, cancellationToken);
  }
}