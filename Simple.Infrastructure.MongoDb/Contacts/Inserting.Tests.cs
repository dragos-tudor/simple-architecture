
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task new_contact_and_message__insert_contact_and_message_transactionally__contact_and_message_stored ()
  {
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [ phoneNumber ]);
    var message = CreateTestMessage() with { MessageContent = default! };

    var db = GetMongoDatabase();
    var contactColl = GetContactCollection(db);
    var messageColl = GetMessageCollection(db);
    await InsertContactAndMessage(contactColl, messageColl, contact, message);

    Assert.IsNotNull(await FindContactByKey(contactColl.AsQueryable(), contact.ContactId).FirstOrDefaultAsync());
    Assert.IsNotNull(await FindMessageByKey(messageColl.AsQueryable(), message.MessageId).FirstOrDefaultAsync());
    Assert.AreEqual(await FindPhoneNumber(contactColl, phoneNumber), phoneNumber);
  }
}