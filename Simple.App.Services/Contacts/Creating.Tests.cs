
using static System.Threading.Tasks.Task;
using NSubstitute;


namespace Simple.App.Services;

partial class ServicesTests
{
  readonly FindModels<PhoneNumber, long> FindPhoneNumbers = Substitute.For<FindModels<PhoneNumber, long>>();
  readonly SaveModels<Contact, Message> SaveContactAndMessage = Substitute.For<SaveModels<Contact, Message>>();
  readonly ProduceMessage<Message> ProduceMessage = Substitute.For<ProduceMessage<Message>>();

  [TestMethod]
  public async Task new_contact__create_contact__contact_saved ()
  {
    var contact = CreateTestContact(contactId: Guid.Empty);
    var saveModels = Substitute.For<SaveModels<Contact, Message>>();
    await CreateContactService(contact, [], FindPhoneNumbers, saveModels, ProduceMessage);

    await saveModels.Received().Invoke(Arg.Is<Contact>(x => x.ContactId == contact.ContactId), Arg.Any<Message<ContactCreatedEvent>>());
  }

  [TestMethod]
  public async Task new_contact__create_contact__contact_created_event_saved ()
  {
    var contact = CreateTestContact(contactId: Guid.Empty);
    var saveModels = Substitute.For<SaveModels<Contact, Message>>();
    await CreateContactService(contact, [], FindPhoneNumbers, saveModels, ProduceMessage);

    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    await saveModels.Received().Invoke(Arg.Any<Contact>(), Arg.Is<Message<ContactCreatedEvent>>(message => message.MessagePayload == contactCreatedEvent));
  }

  [TestMethod]
  public async Task new_contact__create_contact__produce_contact_created_event_message ()
  {
    var contact = CreateTestContact(contactId: Guid.Empty);
    var producehMessage = Substitute.For<ProduceMessage<Message>>();
    await CreateContactService(contact, [], FindPhoneNumbers, SaveContactAndMessage, producehMessage);

    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    await producehMessage.Received().Invoke(Arg.Is<Message<ContactCreatedEvent>>(message => message.MessagePayload == contactCreatedEvent));
  }

  [TestMethod]
  public async Task new_contact_with_existing_phone_number__create_contact__duplicate_phone_number_error ()
  {
    var contact = CreateTestContact(contactId: Guid.Empty);
    PhoneNumber[] phoneNumbers = [CreateTestPhoneNumber()];
    var findPhoneNumbers = Substitute.For<FindModels<PhoneNumber, long>>();

    findPhoneNumbers(phoneNumbers).Returns((_) => FromResult(phoneNumbers.Select(e => e.Number)));
    var result = await CreateContactService(contact, phoneNumbers, findPhoneNumbers, SaveContactAndMessage, ProduceMessage);

    AreEqual(FromFailure(result)!, [GetDuplicatePhoneNumberError(phoneNumbers[0].Number)]);
  }

  [TestMethod]
  public async Task new_contact_with_invalid_contact_email__create_contact__invalid_contact_email_error ()
  {
    var contact = CreateTestContact(contactEmail: "wrong email");
    var result = await CreateContactService(contact, [], FindPhoneNumbers, SaveContactAndMessage, ProduceMessage);

    AreEqual(FromFailure(result)!, [GetInvalidContactEmailError(contact.ContactEmail)]);
  }
}