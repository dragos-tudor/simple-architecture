
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static int CreateSqlDatabase<TContext> (TContext context, string databaseName) where TContext: DbContext =>
    context.Database.ExecuteSqlRaw(GetCreateSqlDatabaseScript(databaseName));
}