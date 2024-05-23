
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static int[] CreateSqlDatabaseUser<TContext> (
    TContext context,
    string databaseName,
    string userName,
    string password) where TContext: DbContext
  => [
    context.Database.ExecuteSqlRaw(GetCreateSqlLoginScript(userName, password)),
    context.Database.ExecuteSqlRaw(GetCreateSqlDatabaseUserScript(databaseName, userName, password))
  ];
}