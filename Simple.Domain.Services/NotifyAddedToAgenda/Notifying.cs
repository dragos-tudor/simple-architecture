
using Microsoft.Extensions.Logging;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<AddedToAgendaNotification?> NotifyAddedToAgendaService (
    Message<ContactCreatedEvent> message,
    string from,
    DateTimeOffset date,
    FindModel<MessageIdempotency, Message?> findDuplicateMessage,
    SendNotification<AddedToAgendaNotification> sendNotification,
    SaveModel<Message<AddedToAgendaNotification>> insertMessage,
    ILogger logger,
    CancellationToken cancellationToken = default)
  {
    var messageIdempotency = CreateMessageIdempotency<AddedToAgendaNotification>(message);
    var parentMessage = await findDuplicateMessage(messageIdempotency, cancellationToken);
    if(ExistMessage(parentMessage)) return default;

    var contactEmail = message.MessagePayload.ContactEmail;
    var notification = CreateAddedToAgendaNotification(from, contactEmail, date);

    await sendNotification(notification, cancellationToken);
    LogNotifiedAddedToAgenda(logger, notification.From, notification.To, message.CorrelationId);

    await insertMessage(CreateChildMessage(message, notification), cancellationToken);
    return notification;
  }
}