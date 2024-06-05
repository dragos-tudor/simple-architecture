
using NSubstitute;

namespace Simple.Domain.Api;

partial class ApiTests
{
  readonly FindModel<Message, Message?> FindParentMessage = Substitute.For<FindModel<Message, Message?>>();
  readonly SendNotification<AddedToAgendaNotification> SendNotification = Substitute.For<SendNotification<AddedToAgendaNotification>>();
  readonly SaveModel<Message<AddedToAgendaNotification>> SaveMessage = Substitute.For<SaveModel<Message<AddedToAgendaNotification>>>();

  [TestMethod]
  public async Task new_contact_created_event__notify_added_to_agenda__added_to_agenda_notified ()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var sendNotification = Substitute.For<SendNotification<AddedToAgendaNotification>>();

    await NotifyAddedToAgendaApi(message, "from", DateTime.MinValue, FindParentMessage, sendNotification, SaveMessage);

    await sendNotification.Received().Invoke(Arg.Is<AddedToAgendaNotification>(notification =>
      notification == CreateAddedToAgendaNotification("from", contact.ContactEmail, DateTime.MinValue)));
  }

  [TestMethod]
  public async Task new_contact_created_event__notify_added_to_agenda__message_with_notification_saved ()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var saveMessage = Substitute.For<SaveModel<Message<AddedToAgendaNotification>>>();

    await NotifyAddedToAgendaApi(message, "from", DateTime.MinValue, FindParentMessage, SendNotification, saveMessage);

    await saveMessage.Received().Invoke(Arg.Is<Message<AddedToAgendaNotification>>(message =>
      message.MessagePayload == CreateAddedToAgendaNotification("from", contact.ContactEmail, DateTime.MinValue)));
  }

  [TestMethod]
  public async Task contact_created_event_already_sent__notify_added_to_agenda__added_to_agenda_not_notified ()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var findParentMessage = Substitute.For<FindModel<Message, Message?>>();
    var sendNotification = Substitute.For<SendNotification<AddedToAgendaNotification>>();

    findParentMessage(message).Returns((_) => Task.FromResult(CreateTestMessage()) as Task<Message?>);
    await NotifyAddedToAgendaApi(message, "from", DateTime.MinValue, findParentMessage, sendNotification, SaveMessage);

    await sendNotification.DidNotReceive().Invoke(Arg.Any<AddedToAgendaNotification>());
  }

  [TestMethod]
  public async Task contact_created_event_already_sent__notify_added_to_agenda__message_with_notification_not_saved ()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var findParentMessage = Substitute.For<FindModel<Message, Message?>>();
    var saveMessage = Substitute.For<SaveModel<Message<AddedToAgendaNotification>>>();

    findParentMessage(message).Returns((_) => Task.FromResult(CreateTestMessage()) as Task<Message?>);
    await NotifyAddedToAgendaApi(message, "from", DateTime.MinValue, findParentMessage, SendNotification, saveMessage);

    await saveMessage.DidNotReceive().Invoke(Arg.Any<Message<AddedToAgendaNotification>>());
  }

}