
using static System.Threading.Tasks.Task;
using NSubstitute;

namespace Simple.Domain.Services;

partial class ServicesTests
{
  readonly FindModel<string, Contact?> FindContactByName = Substitute.For<FindModel<string, Contact?>>();
  readonly FindModel<string, Contact?> FindContactByEmail = Substitute.For<FindModel<string, Contact?>>();
  readonly StoreModels<Contact, Message<ContactCreatedEvent>> InsertContactAndMessage = Substitute.For<StoreModels<Contact, Message<ContactCreatedEvent>>>();
  readonly EnqueueMessage<Message<ContactCreatedEvent>> EnqueueMessage = Substitute.For<EnqueueMessage<Message<ContactCreatedEvent>>>();

  [TestMethod]
  public async Task new_contact__create_contact__contact_saved()
  {
    var contact = CreateTestContact();
    var insertContactAndMessage = Substitute.For<StoreModels<Contact, Message<ContactCreatedEvent>>>();
    await CreateContactService(contact, FindContactByName, FindContactByEmail, insertContactAndMessage, EnqueueMessage, "");

    await insertContactAndMessage.Received().Invoke(
      Arg.Is<Contact>(x => x.ContactId == contact.ContactId),
      Arg.Any<Message<ContactCreatedEvent>>());
  }

  [TestMethod]
  public async Task new_contact__create_contact__contact_created_event_saved()
  {
    var contact = CreateTestContact();
    var insertContactAndMessage = Substitute.For<StoreModels<Contact, Message<ContactCreatedEvent>>>();
    await CreateContactService(contact, FindContactByName, FindContactByEmail, insertContactAndMessage, EnqueueMessage, "");

    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    await insertContactAndMessage.Received().Invoke(
      Arg.Any<Contact>(),
      Arg.Is<Message<ContactCreatedEvent>>(message => message.MessagePayload == contactCreatedEvent));
  }

  [TestMethod]
  public async Task new_contact__create_contact__enqueue_contact_created_event_message()
  {
    var contact = CreateTestContact();
    var enqueueMessage = Substitute.For<EnqueueMessage<Message<ContactCreatedEvent>>>();
    await CreateContactService(contact, FindContactByName, FindContactByEmail, InsertContactAndMessage, enqueueMessage, "");

    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    enqueueMessage.Received().Invoke(
      Arg.Is<Message<ContactCreatedEvent>>(message => message.MessagePayload == contactCreatedEvent));
  }

  [TestMethod]
  public async Task new_contact_existing_contact_name__create_contact__duplicate_contact_name_error()
  {
    var contact = CreateTestContact();
    var findContactByName = Substitute.For<FindModel<string, Contact?>>();

    findContactByName(contact.ContactName).Returns((_) => FromResult(contact) as Task<Contact?>);
    var (_, error) = await CreateContactService(contact, findContactByName, FindContactByEmail, InsertContactAndMessage, EnqueueMessage, "");

    Assert.AreEqual(DuplicateContactNameError, error!);
  }

  [TestMethod]
  public async Task new_contact_existing_contact_email__create_contact__duplicate_contact_name_error()
  {
    var contact = CreateTestContact();
    var findContactByEmail = Substitute.For<FindModel<string, Contact?>>();

    findContactByEmail(contact.ContactEmail).Returns((_) => FromResult(contact) as Task<Contact?>);
    var (_, error) = await CreateContactService(contact, FindContactByName, findContactByEmail, InsertContactAndMessage, EnqueueMessage, "");

    Assert.AreEqual(DuplicateContactEmailError, error!);
  }

  [TestMethod]
  public async Task new_contact_with_invalid_contact_email__create_contact__invalid_contact_email_error()
  {
    var contact = CreateTestContact(contactEmail: "wrong email");
    var (_, error) = await CreateContactService(contact, FindContactByName, FindContactByEmail, InsertContactAndMessage, EnqueueMessage, "");

    Assert.AreEqual(InvalidContactEmailError, error!);
  }
}