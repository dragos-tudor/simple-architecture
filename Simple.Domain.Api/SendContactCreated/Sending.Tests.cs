
using NSubstitute;

namespace Simple.Domain.Api;

partial class ApiTests
{
  readonly FindModel<Message, Message?> FindParentMessage = Substitute.For<FindModel<Message, Message?>>();
  readonly SendNotification<AddedToAgendaNotification> SendNotification = Substitute.For<SendNotification<AddedToAgendaNotification>>();
  readonly SaveMessage<AddedToAgendaNotification> SaveMessage = Substitute.For<SaveMessage<AddedToAgendaNotification>>();

  [TestMethod]
  public async Task new_contact_created_event__send_added_to_agenda_notification__added_to_agenda_notification_sent ()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var sendNotification = Substitute.For<SendNotification<AddedToAgendaNotification>>();

    await SendAddedToAgendaApi(message, "agenda owner", FindParentMessage, sendNotification, SaveMessage);

    await sendNotification.Received().Invoke(Arg.Is<AddedToAgendaNotification>(notification =>
      notification == CreateAddedToAgendaNotification("agenda owner", contact.ContactEmail)));
  }

  [TestMethod]
  public async Task new_contact_created_event__send_added_to_agenda_notification__message_with_notification_saved ()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var saveMessage = Substitute.For<SaveMessage<AddedToAgendaNotification>>();

    await SendAddedToAgendaApi(message, "agenda owner", FindParentMessage, SendNotification, saveMessage);

    await saveMessage.Received().Invoke(Arg.Is<Message<AddedToAgendaNotification>>(message =>
      message.MessagePayload == CreateAddedToAgendaNotification("agenda owner", contact.ContactEmail)));
  }

  [TestMethod]
  public async Task contact_created_event_already_sent__send_added_to_agenda_notification__notification_not_sent ()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var findParentMessage = Substitute.For<FindModel<Message, Message?>>();
    var sendNotification = Substitute.For<SendNotification<AddedToAgendaNotification>>();

    findParentMessage(message).Returns((_) => Task.FromResult(CreateTestMessage()) as Task<Message?>);
    await SendAddedToAgendaApi(message, "agenda owner", findParentMessage, sendNotification, SaveMessage);

    await sendNotification.DidNotReceive().Invoke(Arg.Any<AddedToAgendaNotification>());
  }

  [TestMethod]
  public async Task contact_created_event_already_sent__send_added_to_agenda_notification__message_with_notification_not_saved ()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var findParentMessage = Substitute.For<FindModel<Message, Message?>>();
    var saveMessage = Substitute.For<SaveMessage<AddedToAgendaNotification>>();

    findParentMessage(message).Returns((_) => Task.FromResult(CreateTestMessage()) as Task<Message?>);
    await SendAddedToAgendaApi(message, "agenda owner", findParentMessage, SendNotification, saveMessage);

    await saveMessage.DidNotReceive().Invoke(Arg.Any<Message<AddedToAgendaNotification>>());
  }

}