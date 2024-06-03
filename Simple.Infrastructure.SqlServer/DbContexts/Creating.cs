
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static DbContext CreateDbContext (DbContextOptions options) => new (options);
}