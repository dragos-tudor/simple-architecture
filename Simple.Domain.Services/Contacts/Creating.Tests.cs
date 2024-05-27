
using static System.Threading.Tasks.Task;
using NSubstitute;

namespace Simple.Domain.Services;

partial class ServicesTests
{
  readonly FindModels<PhoneNumber, long> FindPhoneNumbers = Substitute.For<FindModels<PhoneNumber, long>>();
  readonly SaveModels<Contact, Message<ContactCreatedEvent>> SaveContactAndMessage = Substitute.For<SaveModels<Contact, Message<ContactCreatedEvent>>>();
  readonly PublishModel<Message<ContactCreatedEvent>> PublishMessage = Substitute.For<PublishModel<Message<ContactCreatedEvent>>>();

  [TestMethod]
  public async Task new_contact__create_contact__result_contact_created_event ()
  {
    var contact = CreateTestContact();
    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    var result = await CreateContactService(contact, FindPhoneNumbers, SaveContactAndMessage, PublishMessage);

    Assert.AreEqual(FromSuccess(result), contactCreatedEvent);
  }

  [TestMethod]
  public async Task new_contact__create_contact__contact_saved ()
  {
    var contact = CreateTestContact();

    var saveModels = Substitute.For<SaveModels<Contact, Message<ContactCreatedEvent>>>();
    var result = await CreateContactService(contact, FindPhoneNumbers, saveModels, PublishMessage);

    await saveModels.Received().Invoke(Arg.Is<Contact>(x => x.ContactId == contact.ContactId), Arg.Any<Message<ContactCreatedEvent>>());
  }

  [TestMethod]
  public async Task new_contact__create_contact__contact_created_event_saved ()
  {
    var contact = CreateTestContact();
    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);

    var saveModels = Substitute.For<SaveModels<Contact, Message<ContactCreatedEvent>>>();
    var result = await CreateContactService(contact, FindPhoneNumbers, saveModels, PublishMessage);

    await saveModels.Received().Invoke(Arg.Any<Contact>(), Arg.Is<Message<ContactCreatedEvent>>(message => message.MessagePayload == contactCreatedEvent));
  }

  [TestMethod]
  public async Task new_contact__create_contact__publish_contact_created_event ()
  {
    var contact = CreateTestContact();
    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);

    var publishMessage = Substitute.For<PublishModel<Message<ContactCreatedEvent>>>();
    var result = await CreateContactService(contact, FindPhoneNumbers, SaveContactAndMessage, publishMessage);

    await publishMessage.Received().Invoke(Arg.Is<Message<ContactCreatedEvent>>(message => message.MessagePayload == contactCreatedEvent));
  }

  [TestMethod]
  public async Task new_contact_with_existing_phone_number__create_contact__duplicate_phone_number_error ()
  {
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);
    var findPhoneNumbers = Substitute.For<FindModels<PhoneNumber, long>>();

    findPhoneNumbers(contact.PhoneNumbers).Returns((_) => FromResult<IEnumerable<long>>([phoneNumber.Number]));
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