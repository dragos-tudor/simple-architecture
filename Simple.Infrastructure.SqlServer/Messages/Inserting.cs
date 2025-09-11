
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task InsertMessageAsync(AgendaContext dbContext, Message message, CancellationToken cancellationToken = default)
  {
    AddMessage(dbContext, message);
    return SaveChangesAsync(dbContext, cancellationToken);
  }
}