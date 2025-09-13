
namespace Simple.Domain.Services;

partial class ServicesTests
{
  [TestMethod]
  public void message_with_serialized_event__restore_message__message_with_desearialized_event()
  {
    var messagePayload = CreateContactCreatedEvent(GetRandomGuid(), GetRandomEmail(24));
    var source = CreateTestMessage(messagePayload: messagePayload);
    var target = RestoreMessage(source);

    Assert.IsInstanceOfType<Message<ContactCreatedEvent>>(target);
    Assert.AreEqual(messagePayload, (target as Message<ContactCreatedEvent>)!.MessagePayload);
  }
}