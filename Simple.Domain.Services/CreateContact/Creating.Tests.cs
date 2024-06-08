
using static System.Threading.Tasks.Task;
using NSubstitute;

namespace Simple.Domain.Services;

partial class ServicesTests
{
  readonly FindModels<PhoneNumber, PhoneNumber> FindPhoneNumbers = Substitute.For<FindModels<PhoneNumber, PhoneNumber>>();
  readonly FindModel<string, Contact?> FindContactByName = Substitute.For<FindModel<string, Contact?>>();
  readonly FindModel<string, Contact?> FindContactByEmail = Substitute.For<FindModel<string, Contact?>>();
  readonly SaveModels<Contact, Message<ContactCreatedEvent>> SaveModels = Substitute.For<SaveModels<Contact, Message<ContactCreatedEvent>>>();
  readonly ProduceMessage<Message<ContactCreatedEvent>> ProduceMessage = Substitute.For<ProduceMessage<Message<ContactCreatedEvent>>>();

  [TestMethod]
  public async Task new_contact__create_contact__contact_saved ()
  {
    var contact = CreateTestContact(contactId: Guid.Empty);
    var saveModels = Substitute.For<SaveModels<Contact, Message<ContactCreatedEvent>>>();
    await CreateContactService(contact, FindPhoneNumbers, FindContactByName, FindContactByEmail, saveModels, ProduceMessage, Logger);

    await saveModels.Received().Invoke(
      Arg.Is<Contact>(x => x.ContactId == contact.ContactId),
      Arg.Any<Message<ContactCreatedEvent>>());
  }

  [TestMethod]
  public async Task new_contact__create_contact__contact_created_event_saved ()
  {
    var contact = CreateTestContact(contactId: Guid.Empty);
    var saveModels = Substitute.For<SaveModels<Contact, Message<ContactCreatedEvent>>>();
    await CreateContactService(contact, FindPhoneNumbers, FindContactByName, FindContactByEmail, saveModels, ProduceMessage, Logger);

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
    await CreateContactService(contact, FindPhoneNumbers, FindContactByName, FindContactByEmail, SaveModels, producehMessage, Logger);

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
    var result = await CreateContactService(contact, findPhoneNumbers, FindContactByName, FindContactByEmail, SaveModels, ProduceMessage, Logger);

    CollectionAssert.AreEqual(FromFailure(result)!, ToArray([GetDuplicatePhoneNumberFailure(contact.PhoneNumbers[0])]), new FailureComparer());
  }

  [TestMethod]
  public async Task new_contact_existing_contact_name__create_contact__duplicate_contact_name_error ()
  {
    var contact = CreateTestContact();
    var findContactByName = Substitute.For<FindModel<string, Contact?>>();

    findContactByName(contact.ContactName).Returns((_) => FromResult(contact) as Task<Contact?>);
    var result = await CreateContactService(contact, FindPhoneNumbers, findContactByName, FindContactByEmail, SaveModels, ProduceMessage, Logger);

    CollectionAssert.AreEqual(FromFailure(result)!, ToArray([GetDuplicateContactNameFailure(contact.ContactName)]), new FailureComparer());
  }

  [TestMethod]
  public async Task new_contact_existing_contact_email__create_contact__duplicate_contact_name_error ()
  {
    var contact = CreateTestContact();
    var findContactByEmail = Substitute.For<FindModel<string, Contact?>>();

    findContactByEmail(contact.ContactEmail).Returns((_) => FromResult(contact) as Task<Contact?>);
    var result = await CreateContactService(contact, FindPhoneNumbers, FindContactByName, findContactByEmail, SaveModels, ProduceMessage, Logger);

    CollectionAssert.AreEqual(FromFailure(result)!, ToArray([GetDuplicateContactEmailFailure(contact.ContactEmail)]), new FailureComparer());
  }

  [TestMethod]
  public async Task new_contact_with_invalid_contact_email__create_contact__invalid_contact_email_error ()
  {
    var contact = CreateTestContact(contactEmail: "wrong email");
    var result = await CreateContactService(contact, FindPhoneNumbers, FindContactByName, FindContactByEmail, SaveModels, ProduceMessage, Logger);

    CollectionAssert.AreEqual(FromFailure(result)!, ToArray([GetInvalidContactEmailFailure(contact.ContactEmail)]), new FailureComparer());
  }
}