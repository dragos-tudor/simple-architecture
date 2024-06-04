
namespace Simple.Domain.Api;

partial class ApiFuncs
{
  public static async Task<AddedToAgendaNotification?> SendAddedToAgendaApi (
    Message<ContactCreatedEvent> message,
    string agendaOwner,
    FindModel<Message, Message?> findParentMessage,
    SendNotification<AddedToAgendaNotification> sendNotification,
    SaveMessage<AddedToAgendaNotification> saveMessage,
    CancellationToken cancellationToken = default)
  {
    var parentMessage = await findParentMessage(message, cancellationToken); // idempotency [partial]
    if(ExistMessage(parentMessage)) return default;

    var contactEmail = message.MessagePayload.ContactEmail;
    var @event = CreateAddedToAgendaNotification(agendaOwner, contactEmail);

    await sendNotification(@event, cancellationToken);
    await saveMessage(CreateFromMessage(@event, message, isActive: false), cancellationToken);

    LogAddedToAgendaNotification(Logger, @event.AddressTo, message.TraceId);
    return @event;
  }
}