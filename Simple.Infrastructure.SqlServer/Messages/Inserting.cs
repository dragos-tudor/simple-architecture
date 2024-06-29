
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task InsertMessageAsync (AgendaContext dbContext, Message message, CancellationToken cancellationToken = default)
  {
    AddEntity(dbContext, message);
    return SaveChanges(dbContext, cancellationToken);
  }
}