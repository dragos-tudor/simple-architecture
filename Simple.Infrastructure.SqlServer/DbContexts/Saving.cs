
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static async Task SaveChangesAndClearContext<TContext> (TContext dbContext) where TContext: DbContext
  {
    await dbContext.SaveChangesAsync();
    dbContext.ChangeTracker.Clear();
  }
}