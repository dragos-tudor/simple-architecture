
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<NotificationSentEvent?> HandleContactCreatedAsync(
    Message<ContactCreatedEvent> message,
    string from,
    DateTime date,
    FindModel<MessageIdempotency, Message?> findMessage,
    SendNotification sendNotification,
    StoreModel<Message<NotificationSentEvent>> insertMessage,
    CancellationToken cancellationToken = default)
  {
    var messageIdempotency = CreateMessageIdempotency<NotificationSentEvent>(message);
    var parentMessage = await findMessage(messageIdempotency, cancellationToken);
    if (ExistsMessage(parentMessage)) return default;

    var contactEmail = message.MessagePayload.ContactEmail;
    var @event = CreateNotificationSentEvent(from, contactEmail, "added to agenda", $"added to {from} agenda", date);

    await sendNotification(@event, cancellationToken);
    await insertMessage(CreateChildMessage(message, @event), cancellationToken);

    return @event;
  }
}