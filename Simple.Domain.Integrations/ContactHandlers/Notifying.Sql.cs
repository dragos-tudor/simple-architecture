
using Microsoft.EntityFrameworkCore;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<Failure?> NotifyAddedToAgendaSqlHandler (
    Message<ContactCreatedEvent> message,
    AgendaContextFactory sqlContextFactory,
    SendNotification<Notification> sendNotification,
    TimeProvider timeProvider,
    ILogger logger,
    CancellationToken cancellationToken = default)
  {
    using var agendaContext = await sqlContextFactory.CreateDbContextAsync(cancellationToken);
    var messages = agendaContext.Messages;
    await NotifyAddedToAgendaService (
      message,
      "dragos.tudor@gmail.com",
      timeProvider.GetUtcNow(),
      (messageIdempotency, cancellationToken) => FindMessageDuplication(messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync(cancellationToken),
      (notification, cancellationToken) => sendNotification(notification, cancellationToken),
      (message, cancellationToken) => InsertMessageAsync(agendaContext, message, cancellationToken),
      logger,
      cancellationToken
    );
    return default;
  }
}
