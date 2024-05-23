
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static DbContextOptions<TContext> CreateDbContextOptions<TContext> (string serverAddress, string dbName, string userName, string password)
    where TContext: DbContext
  =>
    SqlFuncs.CreateSqlContextOptions<TContext>(
      CreateSqlConnectionString(dbName, serverAddress, userName, password));

  public static DbContextOptions<AgendaContext> CreateAgendaContextOptions (string serverAddress, string databaseName, string userName, string password) =>
    CreateDbContextOptions<AgendaContext>(serverAddress, databaseName, userName, password);

  public static DbContextOptions<DbContext> CreateMasterContextOptions (string serverAddress, string userName, string password) =>
    CreateDbContextOptions<DbContext>(serverAddress, "master", userName, password);
}