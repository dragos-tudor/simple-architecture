
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task new_contact_and_message__insert_contact_and_message__contact_and_message_stored ()
  {
    var contacts = GetContactCollection(Database);
    var messages = GetMessageCollection(Database);

    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [ phoneNumber ]);
    var message = CreateTestMessage() with { MessageContent = default! };
    await InsertContactAndMessage(contacts, messages, contact, message);

    Assert.IsNotNull(await FindContactByKey(contacts.AsQueryable(), contact.ContactId).FirstOrDefaultAsync());
    Assert.IsNotNull(await FindMessageByKey(messages.AsQueryable(), message.MessageId).FirstOrDefaultAsync());
    Assert.AreEqual(await FindPhoneNumber(contacts.AsQueryable(), phoneNumber), phoneNumber);
  }

 [TestMethod]
  public async Task contact_without_phone_numbers__insert_phone_number__phone_number_added_on_contact ()
  {
    var contacts = GetContactCollection(Database);

    var contact = CreateTestContact();
    await InsertContact(contacts, contact);

    var phoneNumber = CreateTestPhoneNumber();
    await InsertContactPhoneNumber(contacts, contact, phoneNumber);

    var actual = await FindContactByKey (contacts.AsQueryable(), contact.ContactId).SingleAsync();
    Assert.AreEqual(actual.PhoneNumbers[0], phoneNumber);
  }

 [TestMethod]
  public async Task contact_with_phone_numbers__insert_phone_number__phone_number_added_on_contact ()
  {
    var contacts = GetContactCollection(Database);

    var contact = CreateTestContact(phoneNumbers: [CreateTestPhoneNumber()]);
    await InsertContact(contacts, contact);

    var phoneNumber = CreateTestPhoneNumber();
    await InsertContactPhoneNumber(contacts, contact, phoneNumber);

    var actual = await FindContactByKey (contacts.AsQueryable(), contact.ContactId).SingleAsync();
    Assert.AreEqual(actual.PhoneNumbers.Count, 2);
    Assert.AreEqual(actual.PhoneNumbers[1], phoneNumber);
  }
}