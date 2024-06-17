
using Microsoft.EntityFrameworkCore;


namespace Simple.Web.Api;

partial class ApiFuncs
{
  static async Task<Failure?> NotifyAddedToAgendaSqlHandler (
    Message<ContactCreatedEvent> message,
    TimeProvider timeProvider,
    AgendaContextFactory agendaContextFactory,
    SendNotification<Notification> sendNotification,
    ILogger logger,
    CancellationToken cancellationToken = default)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync(cancellationToken);
    await NotifyAddedToAgendaService (
      message,
      "dragos.tudor@gmail.com",
      timeProvider.GetUtcNow(),
      (messageIdempotency, cancellationToken) => FindMessageDuplication(agendaContext.Messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync(cancellationToken),
      (notification, cancellationToken) => sendNotification(notification, cancellationToken),
      (message, cancellationToken) => InsertMessage(agendaContext, message, cancellationToken),
      logger,
      cancellationToken
    );
    return default;
  }
}
