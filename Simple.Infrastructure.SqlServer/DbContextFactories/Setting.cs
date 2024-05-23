
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static PooledDbContextFactory<AgendaContext> AgendaContextFactory = default!;

  public static PooledDbContextFactory<AgendaContext> SetAgendaContextFactory (PooledDbContextFactory<AgendaContext> agendaContextFactory) =>
    AgendaContextFactory = agendaContextFactory;
}