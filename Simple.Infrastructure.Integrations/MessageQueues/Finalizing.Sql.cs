
namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  static async Task FinalizeSqlMessage (Message message, AgendaContextFactory agendaContextFactory, CancellationToken cancellationToken = default)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync(cancellationToken);
    await UpdateMessageIsActive(agendaContext, message, false, cancellationToken);
  }
}