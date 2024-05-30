
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task<int> CreateSqlDatabaseAsync<TContext> (TContext context, string databaseName, CancellationToken cancellationToken = default) where TContext: DbContext =>
    context.Database.ExecuteSqlRawAsync (GetCreateSqlDatabaseScript(databaseName), cancellationToken);
}