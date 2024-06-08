
using Microsoft.EntityFrameworkCore;


namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static async Task<Failure?> NotifyAddedToAgendaSqlHandler (
    Message<ContactCreatedEvent> message,
    TimeProvider timeProvider,
    AgendaContextFactory agendaContextFactory,
    SendNotification<Notification> sendNotification,
    CancellationToken cancellationToken = default)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync(cancellationToken);
    await NotifyAddedToAgendaService (
      message,
      "dragos.tudor@gmail.com",
      timeProvider.GetUtcNow(),
      (message, cancellationToken) => FindMessageByParent(agendaContext.Messages, message.MessageId).FirstOrDefaultAsync(cancellationToken),
      (notification, cancellationToken) => sendNotification(notification, cancellationToken),
      (message, cancellationToken) => InsertMessage(agendaContext, message, cancellationToken),
      cancellationToken
    );
    return default;
  }
}
