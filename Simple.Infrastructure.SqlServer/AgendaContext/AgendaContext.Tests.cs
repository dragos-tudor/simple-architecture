
namespace Simple.Infrastructure.SqlServer;

partial class AgendaContext
{
  // support dotnet-ef script command
  // TODO: dotnet-ef migrations support
  public AgendaContext(): base(CreateAgendaContextOptions(string.Empty, string.Empty, string.Empty, string.Empty)) { }
}