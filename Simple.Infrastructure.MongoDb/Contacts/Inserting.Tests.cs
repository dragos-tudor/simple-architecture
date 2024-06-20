
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task new_contact__insert_contact__contact_stored ()
  {
    var contacts = GetContactCollection(AgendaDb);

    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [ phoneNumber ]);
    await InsertContact(contacts, contact);

    Assert.IsNotNull(await FindContactByKey(contacts.AsQueryable(), contact.ContactId).FirstOrDefaultAsync());
    Assert.AreEqual(await FindPhoneNumber(contacts.AsQueryable(), phoneNumber), phoneNumber);
  }

  [TestMethod]
  public async Task new_contact_and_message__insert_contact_and_message__contact_and_message_stored ()
  {
    var contacts = GetContactCollection(AgendaDb);
    var messages = GetMessageCollection(AgendaDb);

    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [ phoneNumber ]);
    var message = CreateTestMessage();
    await InsertContactAndMessage(contacts, messages, contact, message);

    Assert.IsNotNull(await FindContactByKey(contacts.AsQueryable(), contact.ContactId).FirstOrDefaultAsync());
    Assert.IsNotNull(await FindMessageByKey(messages.AsQueryable(), message.MessageId).FirstOrDefaultAsync());
    Assert.AreEqual(await FindPhoneNumber(contacts.AsQueryable(), phoneNumber), phoneNumber);
  }
}