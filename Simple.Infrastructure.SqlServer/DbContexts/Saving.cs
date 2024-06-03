
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task SaveChanges<TContext> (TContext dbContext, CancellationToken cancellationToken = default) where TContext: DbContext => dbContext.SaveChangesAsync(cancellationToken);
}