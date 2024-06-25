
using Microsoft.EntityFrameworkCore;

namespace Simple.App.Services;

partial class ServicesFuncs
{
  public static async Task<Failure?> NotifyAddedToAgendaSqlHandler (
    Message<ContactCreatedEvent> message,
    AgendaContextFactory agendaContextFactory,
    SendNotification<Notification> sendNotification,
    TimeProvider timeProvider,
    ILogger logger,
    CancellationToken cancellationToken = default)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync(cancellationToken);
    var messages = agendaContext.Messages;
    await NotifyAddedToAgendaService (
      message,
      "dragos.tudor@gmail.com",
      timeProvider.GetUtcNow(),
      (messageIdempotency, cancellationToken) => FindMessageDuplication(messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync(cancellationToken),
      (notification, cancellationToken) => sendNotification(notification, cancellationToken),
      (message, cancellationToken) => InsertMessage(agendaContext, message, cancellationToken),
      logger,
      cancellationToken
    );
    return default;
  }
}