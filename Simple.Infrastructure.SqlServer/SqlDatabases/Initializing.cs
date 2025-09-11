
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static void InitializeSqlDatabase(
    string dbName,
    string adminName,
    string adminPassword,
    string userName,
    string userPassword,
    string serverName)
  {
    string connString = CreateSqlConnectionString(dbName, adminName, adminPassword, serverName);
    using var dbContext = CreateAgendaContext(connString);

    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();

    CreateSqlDatabaseUser(dbContext, dbName, userName, userPassword);
  }
}