
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static AgendaContext CreateAgendaContext(string connString) => new(CreateSqlContextOptions<AgendaContext>(connString));
}