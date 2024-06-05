
using System.Reflection;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static IEnumerable<string> MigrateSqlDatabase<TContext> (TContext dbContext, string databaseName, string directory = "SqlMigrations") where TContext: DbContext =>
    ReadSqlMigrations(directory)
      .Select(sqlMigration => ApplySqlMigration(dbContext, ReplaceSqlMigrationToken(sqlMigration, "#database", databaseName)))
      .ToList();

  public static IEnumerable<string> MigrateSqlDatabase<TContext> (TContext dbContext, string databaseName, Assembly assembly) where TContext: DbContext =>
    ReadSqlMigrations(assembly)
      .Select(sqlMigration => ApplySqlMigration(dbContext, ReplaceSqlMigrationToken(sqlMigration, "#database", databaseName)))
      .ToList();
}