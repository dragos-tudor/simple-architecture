
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static void MigrateSqlDatabase<TContext> (TContext context, string databaseName, string directory = "SqlMigrations") where TContext: DbContext
  {
    foreach (var sqlMigration in ReadSqlMigrations(directory))
      context.Database.ExecuteSqlRaw(ReplaceSqlMigrationToken(sqlMigration, "#database", databaseName));
  }
}