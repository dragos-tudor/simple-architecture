
namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  static async Task FinalizeSqlMessage (Message message, AgendaContextFactory sqlContextFactory, CancellationToken cancellationToken = default)
  {
    using var agendaContext = await sqlContextFactory.CreateDbContextAsync(cancellationToken);
    await UpdateMessageIsActiveAsync(agendaContext, message, false, cancellationToken);
  }
}