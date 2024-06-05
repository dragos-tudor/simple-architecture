
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static async Task CreateSqlDatabaseUserAsync<TContext> (TContext context, string databaseName, string userName, string password, CancellationToken cancellationToken = default) where TContext: DbContext
  {
    await context.Database.ExecuteSqlRawAsync (GetCreateSqlLoginScript(userName, password), cancellationToken);
    await context.Database.ExecuteSqlRawAsync (GetCreateSqlDatabaseUserScript(databaseName, userName), cancellationToken);
  }
}