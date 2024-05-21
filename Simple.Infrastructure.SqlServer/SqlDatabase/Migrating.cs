
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static void MigrateDatabase (AgendaContext context, string directory = "SqlMigrations")
  {
    foreach (var sqlMigration in ReadSqlMigrations(directory))
      context.Database.ExecuteSqlRaw(sqlMigration);
  }
}