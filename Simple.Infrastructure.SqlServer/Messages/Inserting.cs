
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task InsertMessage (AgendaContext dbContext, Message message, CancellationToken cancellationToken = default)
  {
    AddEntity(dbContext, message);
    return SaveChanges(dbContext, cancellationToken);
  }
}