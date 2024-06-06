
using static System.Threading.Tasks.Task;
using NSubstitute;

namespace Simple.Domain.Api;

partial class ApiTests
{
  readonly FindModels<PhoneNumber, PhoneNumber> FindPhoneNumbers = Substitute.For<FindModels<PhoneNumber, PhoneNumber>>();
  readonly SaveModels<Contact, Message<ContactCreatedEvent>> SaveModels = Substitute.For<SaveModels<Contact, Message<ContactCreatedEvent>>>();
  readonly ProduceMessage<Message<ContactCreatedEvent>> ProduceMessage = Substitute.For<ProduceMessage<Message<ContactCreatedEvent>>>();

  [TestMethod]
  public async Task new_contact__create_contact__contact_saved ()
  {
    var contact = CreateTestContact(contactId: Guid.Empty);
    var saveModels = Substitute.For<SaveModels<Contact, Message<ContactCreatedEvent>>>();
    await CreateContactApi(contact, FindPhoneNumbers, saveModels, ProduceMessage);

    await saveModels.Received().Invoke(
      Arg.Is<Contact>(x => x.ContactId == contact.ContactId),
      Arg.Any<Message<ContactCreatedEvent>>());
  }

  [TestMethod]
  public async Task new_contact__create_contact__contact_created_event_saved ()
  {
    var contact = CreateTestContact(contactId: Guid.Empty);
    var saveModels = Substitute.For<SaveModels<Contact, Message<ContactCreatedEvent>>>();
    await CreateContactApi(contact, FindPhoneNumbers, saveModels, ProduceMessage);

    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    await saveModels.Received().Invoke(
      Arg.Any<Contact>(),
      Arg.Is<Message<ContactCreatedEvent>>(message => message.MessagePayload == contactCreatedEvent));
  }

  [TestMethod]
  public async Task new_contact__create_contact__produce_contact_created_event_message ()
  {
    var contact = CreateTestContact(contactId: Guid.Empty);
    var producehMessage = Substitute.For<ProduceMessage<Message<ContactCreatedEvent>>>();
    await CreateContactApi(contact, FindPhoneNumbers, SaveModels, producehMessage);

    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    producehMessage.Received().Invoke(
      Arg.Is<Message<ContactCreatedEvent>>(message => message.MessagePayload == contactCreatedEvent));
  }

  [TestMethod]
  public async Task new_contact_with_existing_phone_number__create_contact__duplicate_phone_number_error ()
  {
    var contact = CreateTestContact(contactId: Guid.Empty, phoneNumbers: [CreateTestPhoneNumber()]);
    var findPhoneNumbers = Substitute.For<FindModels<PhoneNumber, PhoneNumber>>();

    findPhoneNumbers(contact.PhoneNumbers).Returns((_) => FromResult(contact.PhoneNumbers.AsEnumerable()));
    var result = await CreateContactApi(contact, findPhoneNumbers, SaveModels, ProduceMessage);

    CollectionAssert.AreEqual(FromFailure(result)!, AsArray([GetDuplicatePhoneNumberError(contact.PhoneNumbers[0])]));
  }

  [TestMethod]
  public async Task new_contact_with_invalid_contact_email__create_contact__invalid_contact_email_error ()
  {
    var contact = CreateTestContact(contactEmail: "wrong email");
    var result = await CreateContactApi(contact, FindPhoneNumbers, SaveModels, ProduceMessage);

    CollectionAssert.AreEqual(FromFailure(result)!, AsArray([GetInvalidContactEmailError(contact.ContactEmail)]));
  }
}