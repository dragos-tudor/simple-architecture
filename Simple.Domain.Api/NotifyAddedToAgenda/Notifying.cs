
namespace Simple.Domain.Api;

partial class ApiFuncs
{
  public static async Task<AddedToAgendaNotification?> NotifyAddedToAgendaApi (
    Message<ContactCreatedEvent> message,
    string from,
    DateTimeOffset date,
    FindModel<Message, Message?> findParentMessage,
    SendNotification<AddedToAgendaNotification> sendNotification,
    SaveModel<Message<AddedToAgendaNotification>> saveMessage,
    CancellationToken cancellationToken = default)
  {
    var parentMessage = await findParentMessage(message, cancellationToken); // idempotency [partial]
    if(ExistMessage(parentMessage)) return default;

    var contactEmail = message.MessagePayload.ContactEmail;
    var notification = CreateAddedToAgendaNotification(from, contactEmail, date);

    await sendNotification(notification, cancellationToken);
    await saveMessage(CreateFromMessage(notification, message, isActive: false), cancellationToken);

    LogAddedToAgendaNotification(Logger, notification.From, notification.To, message.TraceId);
    return notification;
  }
}