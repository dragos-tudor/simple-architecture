
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  internal static PooledDbContextFactory<AgendaContext> AgendaContextFactory = default!;

  public static AgendaContext CreateAgendaContext () => AgendaContextFactory.CreateDbContext();

  public static DbContext CreateMasterContext (string serverAddress, string adminName, string adminPassword) => new (CreateMasterContextOptions(serverAddress, adminName, adminPassword));
}