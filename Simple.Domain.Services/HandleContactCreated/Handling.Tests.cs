
using NSubstitute;

namespace Simple.Domain.Services;

partial class ServicesTests
{
  readonly FindModel<MessageIdempotency, Message?> FindDuplicateMessage = Substitute.For<FindModel<MessageIdempotency, Message?>>();
  readonly SendNotification SendNotification = Substitute.For<SendNotification>();
  readonly StoreModel<Message<NotificationSentEvent>> InsertMessage = Substitute.For<StoreModel<Message<NotificationSentEvent>>>();

  [TestMethod]
  public async Task contact_created_event__handle_contact_created_event__notification_sent()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var sendNotification = Substitute.For<SendNotification>();

    await HandleContactCreatedAsync(message, "from", DateTime.MinValue, FindDuplicateMessage, sendNotification, InsertMessage);

    await sendNotification.Received().Invoke(Arg.Is<NotificationSentEvent>(notification =>
      notification == CreateNotificationSentEvent("from", contact.ContactEmail, "added to agenda", $"added to from agenda", DateTime.MinValue)));
  }

  [TestMethod]
  public async Task contact_created_event__handle_contact_created_event__notification_sent_event_stored()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var insertMessage = Substitute.For<StoreModel<Message<NotificationSentEvent>>>();

    await HandleContactCreatedAsync(message, "from", DateTime.MinValue, FindDuplicateMessage, SendNotification, insertMessage);

    await insertMessage.Received().Invoke(Arg.Is<Message<NotificationSentEvent>>(message =>
      message.MessagePayload == CreateNotificationSentEvent("from", contact.ContactEmail, "added to agenda", $"added to from agenda", DateTime.MinValue)));
  }

  [TestMethod]
  public async Task handled_contact_created_event__handle_contact_created_event__skip_handling_event()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var findDuplicateMessage = Substitute.For<FindModel<MessageIdempotency, Message?>>();
    var sendNotification = Substitute.For<SendNotification>();

    var messageIdempotency = CreateMessageIdempotency<NotificationSentEvent>(message);
    findDuplicateMessage(messageIdempotency).Returns((_) => Task.FromResult(CreateTestMessage()) as Task<Message?>);
    await HandleContactCreatedAsync(message, "from", DateTime.MinValue, findDuplicateMessage, sendNotification, InsertMessage);

    await sendNotification.DidNotReceive().Invoke(Arg.Any<NotificationSentEvent>());
  }

  [TestMethod]
  public async Task handled_contact_created_event__handle_contact_created_event__message_with_notification_not_saved()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var findDuplicateMessage = Substitute.For<FindModel<MessageIdempotency, Message?>>();
    var insertMessage = Substitute.For<StoreModel<Message<NotificationSentEvent>>>();

    var messageIdempotency = CreateMessageIdempotency<NotificationSentEvent>(message);
    findDuplicateMessage(messageIdempotency).Returns((_) => Task.FromResult(CreateTestMessage()) as Task<Message?>);
    await HandleContactCreatedAsync(message, "from", DateTime.MinValue, findDuplicateMessage, SendNotification, insertMessage);

    await insertMessage.DidNotReceive().Invoke(Arg.Any<Message<NotificationSentEvent>>());
  }

}