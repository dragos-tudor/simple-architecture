
using Microsoft.Extensions.Logging;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<AddedToAgendaNotification?> NotifyAddedToAgendaService (
    Message<ContactCreatedEvent> message,
    string from,
    DateTimeOffset date,
    FindModel<Message, Message?> findParentMessage,
    SendNotification<AddedToAgendaNotification> sendNotification,
    SaveModel<Message<AddedToAgendaNotification>> insertMessage,
    ILogger logger,
    CancellationToken cancellationToken = default)
  {
    var parentMessage = await findParentMessage(message, cancellationToken); // idempotency [partial]
    if(ExistMessage(parentMessage)) return default;

    var contactEmail = message.MessagePayload.ContactEmail;
    var notification = CreateAddedToAgendaNotification(from, contactEmail, date);

    await sendNotification(notification, cancellationToken);
    LogNotifiedAddedToAgenda(logger, notification.From, notification.To, message.TraceId);

    await insertMessage(CreateFromMessage(notification, message, isActive: false), cancellationToken);
    return notification;
  }
}