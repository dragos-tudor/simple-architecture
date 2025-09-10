
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  public static void ClearChangeTracker<TContext>(TContext dbContext) where TContext : DbContext => dbContext.ChangeTracker.Clear();
}