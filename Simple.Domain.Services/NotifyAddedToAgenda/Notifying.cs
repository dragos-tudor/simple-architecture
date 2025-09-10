
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<AddedToAgendaNotification?> NotifyAddedToAgendaService(
    Message<ContactCreatedEvent> message,
    string from,
    DateTimeOffset date,
    FindModel<MessageIdempotency, Message?> findDuplicateMessage,
    SendNotification<AddedToAgendaNotification> sendNotification,
    StoreModel<Message<AddedToAgendaNotification>> insertMessage,
    CancellationToken cancellationToken = default)
  {
    var messageIdempotency = CreateMessageIdempotency<AddedToAgendaNotification>(message);
    var parentMessage = await findDuplicateMessage(messageIdempotency, cancellationToken);
    if (ExistsMessage(parentMessage)) return default;

    var contactEmail = message.MessagePayload.ContactEmail;
    var notification = CreateAddedToAgendaNotification(from, contactEmail, date);

    await sendNotification(notification, cancellationToken);
    await insertMessage(CreateChildMessage(message, notification), cancellationToken);

    return notification;
  }
}