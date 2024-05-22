
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  const string DefaultDatabaseName = "agenda";

  internal static AgendaContext CreateAgendaContext (string dbName = DefaultDatabaseName) =>
    new (CreateAgendaContextOptions(dbName));

  internal static DbContextOptions<AgendaContext> CreateAgendaContextOptions (string dbName = DefaultDatabaseName) =>
    CreateDbContextOptions<AgendaContext>(dbName);
}