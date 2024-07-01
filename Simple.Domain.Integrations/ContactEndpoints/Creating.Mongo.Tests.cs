
using MongoDB.Driver;

namespace Simple.Domain.Integrations;

partial class IntegrationsTests
{
  [TestMethod]
  public async Task new_contact_with_phone_number__create_mongo_contact__contact_with_phone_number_created ()
  {
    var contacts = GetContactCollection(MongoDatabase);
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);
    var messageQueue = CreateMessageQueue<Message>();

    var response = await CreateContactMongoEndpoint(contact, MongoDatabase, messageQueue, new DefaultHttpContext(), Logger);

    var actual = await FindContactByKey(contacts.AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    Assert.AreEqual(actual, contact);
    Assert.IsInstanceOfType(response.Result, typeof(Created));
  }

  [TestMethod]
  public async Task new_contact_with_phone_number__create_mongo_contact__contact_created_event_message_queued ()
  {
    var contacts = GetContactCollection(MongoDatabase);
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);
    var messageQueue = CreateMessageQueue<Message>();

    await CreateContactMongoEndpoint(contact, MongoDatabase, messageQueue, new DefaultHttpContext(), Logger);

    var actual = await DequeueMessage(messageQueue);
    Assert.AreEqual(actual.MessageType, typeof(ContactCreatedEvent).Name);
  }
}