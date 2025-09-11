
using NSubstitute;

namespace Simple.Domain.Services;

partial class ServicesTests
{
  readonly FindModel<MessageIdempotency, Message?> FindDuplicateMessage = Substitute.For<FindModel<MessageIdempotency, Message?>>();
  readonly SendNotification<AddedToAgendaNotification> SendNotification = Substitute.For<SendNotification<AddedToAgendaNotification>>();
  readonly StoreModel<Message<AddedToAgendaNotification>> InsertMessage = Substitute.For<StoreModel<Message<AddedToAgendaNotification>>>();

  [TestMethod]
  public async Task new_contact_created_event__notify_added_to_agenda__added_to_agenda_notified()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var sendNotification = Substitute.For<SendNotification<AddedToAgendaNotification>>();

    await NotifyAddedToAgendaAsync(message, "from", DateTime.MinValue, FindDuplicateMessage, sendNotification, InsertMessage);

    await sendNotification.Received().Invoke(Arg.Is<AddedToAgendaNotification>(notification =>
      notification == CreateAddedToAgendaNotification("from", contact.ContactEmail, DateTime.MinValue)));
  }

  [TestMethod]
  public async Task new_contact_created_event__notify_added_to_agenda__message_with_notification_saved()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var insertMessage = Substitute.For<StoreModel<Message<AddedToAgendaNotification>>>();

    await NotifyAddedToAgendaAsync(message, "from", DateTime.MinValue, FindDuplicateMessage, SendNotification, insertMessage);

    await insertMessage.Received().Invoke(Arg.Is<Message<AddedToAgendaNotification>>(message =>
      message.MessagePayload == CreateAddedToAgendaNotification("from", contact.ContactEmail, DateTime.MinValue)));
  }

  [TestMethod]
  public async Task contact_created_event_already_sent__notify_added_to_agenda__added_to_agenda_not_notified()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var findDuplicateMessage = Substitute.For<FindModel<MessageIdempotency, Message?>>();
    var sendNotification = Substitute.For<SendNotification<AddedToAgendaNotification>>();

    var messageIdempotency = CreateMessageIdempotency<AddedToAgendaNotification>(message);
    findDuplicateMessage(messageIdempotency).Returns((_) => Task.FromResult(CreateTestMessage()) as Task<Message?>);
    await NotifyAddedToAgendaAsync(message, "from", DateTime.MinValue, findDuplicateMessage, sendNotification, InsertMessage);

    await sendNotification.DidNotReceive().Invoke(Arg.Any<AddedToAgendaNotification>());
  }

  [TestMethod]
  public async Task contact_created_event_already_sent__notify_added_to_agenda__message_with_notification_not_saved()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var findDuplicateMessage = Substitute.For<FindModel<MessageIdempotency, Message?>>();
    var insertMessage = Substitute.For<StoreModel<Message<AddedToAgendaNotification>>>();

    var messageIdempotency = CreateMessageIdempotency<AddedToAgendaNotification>(message);
    findDuplicateMessage(messageIdempotency).Returns((_) => Task.FromResult(CreateTestMessage()) as Task<Message?>);
    await NotifyAddedToAgendaAsync(message, "from", DateTime.MinValue, findDuplicateMessage, SendNotification, insertMessage);

    await insertMessage.DidNotReceive().Invoke(Arg.Any<Message<AddedToAgendaNotification>>());
  }

}