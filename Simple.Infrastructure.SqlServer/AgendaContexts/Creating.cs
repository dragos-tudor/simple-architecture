
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static AgendaContext CreateAgendaContext(string connString) => new(CreateSqlContextOptions<AgendaContext>(connString));

  public static AgendaContext CreateAgendaContext(AgendaContextFactory agendaContextFactory) => agendaContextFactory.CreateDbContext();

  public static AgendaContextFactory CreateAgendaContextFactory(string connString) => new(CreateSqlContextOptions<AgendaContext>(connString));

  public static AgendaContextFactory CreateAgendaContextFactory(SqlServerOptions options) => new(CreateSqlContextOptions<AgendaContext>(CreateSqlConnectionString(options)));
}