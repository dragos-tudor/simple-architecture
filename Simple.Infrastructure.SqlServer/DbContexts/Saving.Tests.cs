
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  public static async Task SaveTestChanges<TContext> (TContext dbContext, CancellationToken cancellationToken = default) where TContext: DbContext
  {
    await dbContext.SaveChangesAsync(cancellationToken);
    ClearChangeTracker(dbContext);
  }
}