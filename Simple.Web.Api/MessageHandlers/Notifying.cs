
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static async Task<string?> NotifyAddedToAgendaSqlHandler (
    Message<ContactCreatedEvent> message,
    TimeProvider timeProvider,
    AgendaContextFactory agendaContextFactory,
    SendNotification<Notification> sendNotification,
    Channel<Message> messageQueue,
    CancellationToken cancellationToken = default)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync(cancellationToken);
    await NotifyAddedToAgendaApi (
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

  internal static async Task<string?> NotifyAddedToAgendaMongoHandler (
    Message<ContactCreatedEvent> message,
    TimeProvider timeProvider,
    IMongoDatabase agendaDdb,
    SendNotification<Notification> sendNotification,
    Channel<Message> messageQueue,
    CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(agendaDdb);
    await NotifyAddedToAgendaApi (
      message,
      "dragos.tudor@gmail.com",
      timeProvider.GetUtcNow(),
      (message, cancellationToken) => (FindMessageByParent(messages.AsQueryable(), message.MessageId) as IQueryable<Message>).FirstOrDefaultAsync(cancellationToken),
      (notification, cancellationToken) => sendNotification(notification, cancellationToken),
      (message, cancellationToken) => InsertMessage(messages, message, cancellationToken),
      cancellationToken
    );
    return default;
  }
}
