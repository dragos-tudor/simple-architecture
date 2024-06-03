
namespace Simple.Infrastructure.SqlServer;

partial class AgendaContext
{
  // support dotnet-ef script command
  // TODO: dotnet-ef migrations support
  public AgendaContext(): base(CreateSqlContextOptions<AgendaContext>("")) { }
}