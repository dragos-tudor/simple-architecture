
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task SaveChangesAsync<TContext>(TContext dbContext, CancellationToken cancellationToken = default) where TContext : DbContext =>
    dbContext.SaveChangesAsync(cancellationToken);
}