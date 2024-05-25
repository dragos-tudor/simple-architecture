
using static System.Threading.Tasks.Task;
using NSubstitute;

namespace Simple.Domain.Services;

partial class ServicesTests
{
  readonly FindPhoneNumbers FindPhoneNumbers = Substitute.For<FindPhoneNumbers>();
  readonly SaveModelAndMessage<Contact, ContactCreatedEvent> SaveContactAndMessage = Substitute.For<SaveModelAndMessage<Contact, ContactCreatedEvent>>();
  readonly PublishMessage<ContactCreatedEvent> PublishMessage = Substitute.For<PublishMessage<ContactCreatedEvent>>();

  [TestMethod]
  public async Task new_contact__create_contact__result_contact_created_event ()
  {
    var contact = CreateTestContact();
    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    var result = await CreateContactService(contact, FindPhoneNumbers, SaveContactAndMessage, PublishMessage);

    Assert.AreEqual(FromSuccess(result), contactCreatedEvent);
  }

  [TestMethod]
  public async Task new_contact__create_contact__contact_is_saved ()
  {
    var contact = CreateTestContact();

    var saveContactAndMessage = Substitute.For<SaveModelAndMessage<Contact, ContactCreatedEvent>>();
    var result = await CreateContactService(contact, FindPhoneNumbers, saveContactAndMessage, PublishMessage);

    await saveContactAndMessage.Received().Invoke(Arg.Is<Contact>(x => x.ContactId == contact.ContactId), Arg.Any<Message<ContactCreatedEvent>>());
  }

  [TestMethod]
  public async Task new_contact__create_contact__message_with_contact_created_event_is_saved ()
  {
    var contact = CreateTestContact();
    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);

    var saveContactAndMessage = Substitute.For<SaveModelAndMessage<Contact, ContactCreatedEvent>>();
    var result = await CreateContactService(contact, FindPhoneNumbers, saveContactAndMessage, PublishMessage);

    await saveContactAndMessage.Received().Invoke(Arg.Any<Contact>(), Arg.Is<Message<ContactCreatedEvent>>(x => x.MessagePayload == contactCreatedEvent));
  }

  [TestMethod]
  public async Task new_contact__create_contact__publish_contact_created_event_message ()
  {
    var contact = CreateTestContact();
    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);

    var publishMessage = Substitute.For<PublishMessage<ContactCreatedEvent>>();
    var result = await CreateContactService(contact, FindPhoneNumbers, SaveContactAndMessage, publishMessage);

    await publishMessage.Received().Invoke(Arg.Is<Message<ContactCreatedEvent>>(x => x.MessagePayload == contactCreatedEvent));
  }

  [TestMethod]
  public async Task new_contact_with_existing_phone_number__create_contact__duplicate_phone_number_error ()
  {
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);
    var findPhoneNumbers = Substitute.For<FindPhoneNumbers>();

    findPhoneNumbers(contact.PhoneNumbers).Returns((_) => FromResult<IEnumerable<string>>([phoneNumber.Number]));
    var result = await CreateContactService(contact, findPhoneNumbers, SaveContactAndMessage, PublishMessage);

    AreEqual(FromFailure(result)!, [GetDuplicatePhoneNumberError(phoneNumber.Number)]);
  }

  [TestMethod]
  public async Task new_contact_with_invalid_contact_email__create_contact__invalid_contact_email_error ()
  {
    var contact = CreateTestContact(contactEmail: "wrong email");
    var result = await CreateContactService(contact, FindPhoneNumbers, SaveContactAndMessage, PublishMessage);

    AreEqual(FromFailure(result)!, [GetInvalidContactEmailError(contact.ContactEmail)]);
  }
}