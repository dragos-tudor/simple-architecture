
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task SaveChanges<TContext> (TContext dbContext, CancellationToken cancellationToken = default) where TContext: DbContext =>
    dbContext.SaveChangesAsync(cancellationToken);

  public static async Task SaveChangesAndClearContext<TContext> (TContext dbContext, CancellationToken cancellationToken = default) where TContext: DbContext
  {
    await dbContext.SaveChangesAsync(cancellationToken);
    dbContext.ChangeTracker.Clear();
  }
}