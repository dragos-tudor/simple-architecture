
using static System.Threading.Tasks.Task;
using NSubstitute;

namespace Simple.Domain.Services;

partial class ServicesTests
{
  readonly FindModels<PhoneNumber, long> FindPhoneNumbers = Substitute.For<FindModels<PhoneNumber, long>>();
  readonly SaveModelAndEvent<Contact, ContactCreatedEvent> SaveContactAndEvent = Substitute.For<SaveModelAndEvent<Contact, ContactCreatedEvent>>();
  readonly PublishEvent<ContactCreatedEvent> PublishContactCreatedEvent = Substitute.For<PublishEvent<ContactCreatedEvent>>();

  [TestMethod]
  public async Task new_contact__create_contact__result_contact_created_event ()
  {
    var contact = CreateTestContact();
    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    var result = await CreateContactService(contact, FindPhoneNumbers, SaveContactAndEvent, PublishContactCreatedEvent);

    Assert.AreEqual(FromSuccess(result), contactCreatedEvent);
  }

  [TestMethod]
  public async Task new_contact__create_contact__contact_saved ()
  {
    var contact = CreateTestContact();

    var saveContactAndEvent = Substitute.For<SaveModelAndEvent<Contact, ContactCreatedEvent>>();
    var result = await CreateContactService(contact, FindPhoneNumbers, saveContactAndEvent, PublishContactCreatedEvent);

    await saveContactAndEvent.Received().Invoke(Arg.Is<Contact>(x => x.ContactId == contact.ContactId), Arg.Any<ContactCreatedEvent>());
  }

  [TestMethod]
  public async Task new_contact__create_contact__contact_created_event_saved ()
  {
    var contact = CreateTestContact();
    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);

    var saveContactAndEvent = Substitute.For<SaveModelAndEvent<Contact, ContactCreatedEvent>>();
    var result = await CreateContactService(contact, FindPhoneNumbers, saveContactAndEvent, PublishContactCreatedEvent);

    await saveContactAndEvent.Received().Invoke(Arg.Any<Contact>(), Arg.Is<ContactCreatedEvent>(@event => @event == contactCreatedEvent));
  }

  [TestMethod]
  public async Task new_contact__create_contact__publish_contact_created_event ()
  {
    var contact = CreateTestContact();
    var contactCreatedEvent = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);

    var publishContactCreatedEvent = Substitute.For<PublishEvent<ContactCreatedEvent>>();
    var result = await CreateContactService(contact, FindPhoneNumbers, SaveContactAndEvent, publishContactCreatedEvent);

    await publishContactCreatedEvent.Received().Invoke(Arg.Is<ContactCreatedEvent>(@event => @event == contactCreatedEvent));
  }

  [TestMethod]
  public async Task new_contact_with_existing_phone_number__create_contact__duplicate_phone_number_error ()
  {
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);
    var findPhoneNumbers = Substitute.For<FindModels<PhoneNumber, long>>();

    findPhoneNumbers(contact.PhoneNumbers).Returns((_) => FromResult<IEnumerable<long>>([phoneNumber.Number]));
    var result = await CreateContactService(contact, findPhoneNumbers, SaveContactAndEvent, PublishContactCreatedEvent);

    AreEqual(FromFailure(result)!, [GetDuplicatePhoneNumberError(phoneNumber.Number)]);
  }

  [TestMethod]
  public async Task new_contact_with_invalid_contact_email__create_contact__invalid_contact_email_error ()
  {
    var contact = CreateTestContact(contactEmail: "wrong email");
    var result = await CreateContactService(contact, FindPhoneNumbers, SaveContactAndEvent, PublishContactCreatedEvent);

    AreEqual(FromFailure(result)!, [GetInvalidContactEmailError(contact.ContactEmail)]);
  }
}