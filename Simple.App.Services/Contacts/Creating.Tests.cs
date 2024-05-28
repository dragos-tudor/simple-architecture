
using static System.Threading.Tasks.Task;
using NSubstitute;


namespace Simple.App.Services;

partial class ServicesTests
{
  readonly FindModels<PhoneNumber, long> FindPhoneNumbers = Substitute.For<FindModels<PhoneNumber, long>>();
  readonly SaveModels<Contact, Message> SaveContactAndMessage = Substitute.For<SaveModels<Contact, Message>>();
  readonly ProduceMessage<Message> ProduceMessage = Substitute.For<ProduceMessage<Message>>();

  [TestMethod]
  public async Task new_contact__create_contact__result_contact_created_event ()
  {
    var contact = CreateTestContact();
    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    var result = await CreateContactService(contact, FindPhoneNumbers, SaveContactAndMessage, ProduceMessage);

    Assert.AreEqual(FromSuccess(result), contactCreatedEvent);
  }

  [TestMethod]
  public async Task new_contact__create_contact__contact_saved ()
  {
    var contact = CreateTestContact();

    var saveModels = Substitute.For<SaveModels<Contact, Message>>();
    var result = await CreateContactService(contact, FindPhoneNumbers, saveModels, ProduceMessage);

    await saveModels.Received().Invoke(Arg.Is<Contact>(x => x.ContactId == contact.ContactId), Arg.Any<Message<ContactCreatedEvent>>());
  }

  [TestMethod]
  public async Task new_contact__create_contact__contact_created_event_saved ()
  {
    var contact = CreateTestContact();
    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);

    var saveModels = Substitute.For<SaveModels<Contact, Message>>();
    var result = await CreateContactService(contact, FindPhoneNumbers, saveModels, ProduceMessage);

    await saveModels.Received().Invoke(Arg.Any<Contact>(), Arg.Is<Message<ContactCreatedEvent>>(message => message.MessagePayload == contactCreatedEvent));
  }

  [TestMethod]
  public async Task new_contact__create_contact__produce_contact_created_event_message ()
  {
    var contact = CreateTestContact();
    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);

    var producehMessage = Substitute.For<ProduceMessage<Message>>();
    var result = await CreateContactService(contact, FindPhoneNumbers, SaveContactAndMessage, producehMessage);

    await producehMessage.Received().Invoke(Arg.Is<Message<ContactCreatedEvent>>(message => message.MessagePayload == contactCreatedEvent));
  }

  [TestMethod]
  public async Task new_contact_with_existing_phone_number__create_contact__duplicate_phone_number_error ()
  {
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);
    var findPhoneNumbers = Substitute.For<FindModels<PhoneNumber, long>>();

    findPhoneNumbers(contact.PhoneNumbers).Returns((_) => FromResult<IEnumerable<long>>([phoneNumber.Number]));
    var result = await CreateContactService(contact, findPhoneNumbers, SaveContactAndMessage, ProduceMessage);

    AreEqual(FromFailure(result)!, [GetDuplicatePhoneNumberError(phoneNumber.Number)]);
  }

  [TestMethod]
  public async Task new_contact_with_invalid_contact_email__create_contact__invalid_contact_email_error ()
  {
    var contact = CreateTestContact(contactEmail: "wrong email");
    var result = await CreateContactService(contact, FindPhoneNumbers, SaveContactAndMessage, ProduceMessage);

    AreEqual(FromFailure(result)!, [GetInvalidContactEmailError(contact.ContactEmail)]);
  }
}