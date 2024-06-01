
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
 [TestMethod]
  public async Task contact_without_phone_numbers__insert_phone_number__phone_number_added_on_contact ()
  {
    var client = GetMongoClient();
    var db = GetMongoDatabase(client);
    var contacts = GetContactCollection(db);

    var contact = CreateTestContact(phoneNumbers: [CreateTestPhoneNumber()]);
    await InsertContact(contacts, contact);

    var actual = await FindContactByKey (contacts.AsQueryable(), contact.ContactId).SingleAsync();
    Assert.AreEqual(actual.PhoneNumbers[0], contact.PhoneNumbers[0]);
  }

 [TestMethod]
  public async Task contact_with_phone_numbers__insert_phone_number__phone_number_added_on_contact ()
  {
    var client = GetMongoClient();
    var db = GetMongoDatabase(client);
    var contacts = GetContactCollection(db);

    var contact = CreateTestContact(phoneNumbers: [CreateTestPhoneNumber()]);
    await InsertContact(contacts, contact);

    var phoneNumber = CreateTestPhoneNumber();
    await InsertPhoneNumber(contacts, contact, phoneNumber);

    var actual = await FindContactByKey (contacts.AsQueryable(), contact.ContactId).SingleAsync();
    Assert.AreEqual(actual.PhoneNumbers.Count, 2);
    Assert.AreEqual(actual.PhoneNumbers[1], phoneNumber);
  }
}