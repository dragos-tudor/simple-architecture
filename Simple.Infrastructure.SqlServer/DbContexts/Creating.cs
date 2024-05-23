
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static AgendaContext CreateAgendaContext () => AgendaContextFactory.CreateDbContext();

  public static DbContext CreateMasterContext (string serverAddress, string adminName, string adminPassword) =>
    new (CreateMasterContextOptions(serverAddress, adminName, adminPassword));
}