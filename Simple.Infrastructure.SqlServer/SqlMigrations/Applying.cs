
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static string ApplySqlMigration<TContext> (TContext dbContext, string sqlMigration) where TContext: DbContext
  {
    dbContext.Database.ExecuteSqlRaw(sqlMigration);
    return sqlMigration;
  }
}