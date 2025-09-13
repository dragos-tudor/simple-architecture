
using MongoDB.Driver;

namespace Simple.Api.Endpoints;

partial class EndpointsTests
{
  [TestMethod]
  public async Task new_contact__create_mongo_contact__contact_created()
  {
    var contacts = GetContactCollection(MongoDatabase);
    var request = CreateTestCreateContactRequest();
    var messageQueue = CreateMessageQueue<Message>();

    var response = await CreateContactMongoAsync(request, MongoDatabase, messageQueue, "", CancellationToken.None);

    var contact = await FindContactByName(contacts.AsQueryable(), request.ContactName).FirstOrDefaultAsync();
    Assert.IsNotNull(contact);
    Assert.IsInstanceOfType(response.Result, typeof(Created));
  }

  [TestMethod]
  public async Task new_contact__create_mongo_contact__contact_created_event_message_queued()
  {
    var request = CreateTestCreateContactRequest();
    var messageQueue = CreateMessageQueue<Message>();

    await CreateContactMongoAsync(request, MongoDatabase, messageQueue, "", CancellationToken.None);

    var actual = await DequeueMessage(messageQueue);
    Assert.AreEqual(actual.MessageType, typeof(ContactCreatedEvent).FullName);
  }
}